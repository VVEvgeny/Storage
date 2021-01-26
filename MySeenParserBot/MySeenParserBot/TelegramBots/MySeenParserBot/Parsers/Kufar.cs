using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Telegram.Bot;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public class Kufar: ParserBase, IParser
    {
        [Serializable]
        private class Item
        {
            public string Name;
            public string Image;
            public string Info;
            public string Link;
            public string Location;
            public string LastUpdate;
            public string Price;
        }

        private static CacheWebItemStorage<Item> Cache = new CacheWebItemStorage<Item>();
        private static List<Item> GetItemsFromWeb(string url)
        {
            if (Cache.ContainsActual(url))
                return Cache.Get(url);

            var items = new List<Item>();

            // From Web
            //var url = "https://www.kufar.by/listings?cat=16010&cct=11&rgn=all";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var nodes = doc.QuerySelectorAll("a .k-fxEn-b80df");
            if (nodes == null || nodes.Count == 0)
                nodes = doc.QuerySelectorAll("a .k-pmsp-e76f1");

            foreach (var node in nodes)
            {
                var item = new Item
                {
                    Name = node.TryGetNode("h3 .k-fxbJ-16e40", "h3 .k-pmvA-ebdbb")?.InnerText,
                    //Name = node.QuerySelector("div .listing-item__about")?.QuerySelector("h3 a span")?.InnerText?.Replace("<!-- -->", ""),
                    Image = node.QuerySelector("img")?.Attributes["data-src"]?.Value,
                    Info = "",
                    Price = node.TryGetNode("span .k-beVe-c6572", "span .k-hNKj-19b8b")?.InnerText,
                    Location = node.TryGetNode("span .k-fwEX-8b3f6", "span .k-pYsu-eb441")?.InnerText,
                    LastUpdate = node.TryGetNode("div .k-fFtb-c33c6", "div .k-pxsl-541ef")?.InnerText,
                    Link = node.Attributes["href"]?.Value
                };

                //ShowItem(item);

                items.Add(item);
            }

            Cache.AddOrUpdate(url, items);

            return items;
        }

        private static string ShowItem(Item item)
        {
            var ret = "";
            ret += "==========ДОБАВЛЕНО===========" + Environment.NewLine;
            ret += "Название: " + item.Name + Environment.NewLine;
            ret += "Изображение: " + item.Image + Environment.NewLine;
            ret += "Хар-ки: " + item.Info + Environment.NewLine;
            ret += "Цена: " + item.Price + Environment.NewLine;
            ret += "Где: " + item.Location + Environment.NewLine;
            ret += "Обновлено: " + item.LastUpdate + Environment.NewLine;
            ret += "Ссылка: " + item.Link + Environment.NewLine;
            return ret;
        }

        private class ItemDiff
        {
            public Item InStorage;
            public Item InWeb;

            public bool NewForThisTime;

            //what changed
            public bool Name;
            public bool Image;
            public bool Info;
            public bool Location;
            public bool LastUpdate = false;
            public bool Price;
        }
        private static List<ItemDiff> CompareItems(List<Item> fromStorage, List<Item> fromWeb)
        {
            var cd = new List<ItemDiff>();
            /*
            if (fromStorage.Count != fromWeb.Count)
            {
                //MessageBox.Show("some strange! different sizes! fromStorage.Count=" + fromStorage.Count +
                //                " fromWeb.Count=" + fromWeb.Count);
                return cd;
            }
            */

            bool hit = false; // определение момента пересечения, чтобы не добавлять ТИПА новое после того как уже когда-то было раньше...
            foreach (var cw in fromWeb)
            {
                var cs = fromStorage.FirstOrDefault(c => c.Link == cw.Link);

                if (cs == null)
                {
                    if (hit != true)//типа добавляем новые в список если они появились в самом начале обновления
                        cd.Add(new ItemDiff { InWeb = cw, NewForThisTime = true });
                }
                else
                {
                    hit = true;
                    if (cs.Name != cw.Name ||
                        cs.Image != cw.Image ||
                        cs.Info != cw.Info ||
                        cs.Location != cw.Location ||
                        //cs.LastUpdate != cw.LastUpdate ||
                        cs.Price != cw.Price
                        )
                    {
                        cd.Add(new ItemDiff
                        {
                            InWeb = cw,
                            InStorage = cs,
                            NewForThisTime = false,
                            Name = cs.Name != cw.Name,
                            Image = cs.Image != cw.Image,
                            Info = cs.Info != cw.Info,
                            Location = cs.Location != cw.Location,
                            //LastUpdate = cs.LastUpdate != cw.LastUpdate,
                            Price = cs.Price != cw.Price
                        });
                    }
                }
            }
            return cd;
        }
        private static string ShowItemChange(ItemDiff card)
        {
            string messageToBot = "";
            messageToBot += "==========ИЗМЕНЕНИЕ===========" + Environment.NewLine;
            messageToBot += "Название: " + card.InWeb.Name + Environment.NewLine;
            messageToBot += "Ссылка: " + card.InWeb.Link + Environment.NewLine;

            if (card.Name)
                messageToBot += "Название Было:" + card.InStorage.Name + " Стало:" + card.InWeb.Name +
                                Environment.NewLine;

            if (card.Image)
                messageToBot += "Изображение Было:" + card.InStorage.Image + " Стало:" + card.InWeb.Image +
                                Environment.NewLine;

            if (card.Info)
                messageToBot += "Хар-ки Было:" + card.InStorage.Info + " Стало:" + card.InWeb.Info +
                                Environment.NewLine;

            if (card.Price)
                messageToBot += "Цена Было:" + card.InStorage.Price + " Стало:" + card.InWeb.Price +
                                Environment.NewLine;

            if (card.Location)
                messageToBot += "Где Было:" + card.InStorage.Location + " Стало:" +
                                card.InWeb.Location + Environment.NewLine;

            if (card.LastUpdate)
                messageToBot += "Обновлено Было:" + card.InStorage.LastUpdate + " Стало:" +
                                card.InWeb.LastUpdate + Environment.NewLine;

            return messageToBot;
        }

        //1й раз не показываем этот огромный список!
        private readonly HashSet<string> _running = new HashSet<string>();
        public async void ProcessTask(long userId, BotTasks.BotTask task, TelegramBotClient bot, CancellationToken cancellationToken,
            BotTasks.SaveDataProcessTaskDelegate saveDataProcessTask, BotTasks.OnDeleteWithParsingDelegate onDeleteWithParsing)
        {
            try
            {
                var silentBecauseFirstRun = !_running.Contains(userId + task.Request);

                var cw = GetItemsFromWeb(task.Request);
                if (!silentBecauseFirstRun)
                {
                    var cs = string.IsNullOrEmpty(task.Data)
                        ? null
                        : JsonConvert.DeserializeObject<List<Item>>(task.Data);

                    if (cs == null || cs.Count == 0)
                    {
                        //first run? storage is empty? just save and show All
                        foreach (var c in cw)
                        {
                            await SendToUser(userId, ShowItem(c), bot, cancellationToken);
                        }
                    }
                    else
                    {
                        var cd = CompareItems(cs, cw);

                        if (cd.Count != 0)
                        {
                            string addData = "Обновлений:" + cd.Count + Environment.NewLine;

                            foreach (var card in cd)
                            {
                                if (card.NewForThisTime)
                                {
                                    await SendToUser(userId, addData + ShowItem(card.InWeb), bot, cancellationToken);
                                }
                                else
                                {
                                    await SendToUser(userId, addData + ShowItemChange(card), bot, cancellationToken);
                                }

                                addData = "";
                            }
                        }
                    }
                }

                //Защита от вылетов инета, если в новом запросе пусто, то нечего тут и делать
                if (cw.Count != 0)
                {
                    //перед обновлением словаря не забыть заблокировать его !!!
                    saveDataProcessTask(userId, task.TaskId, JsonConvert.SerializeObject(cw));
                }

                if (silentBecauseFirstRun)
                    _running.Add(userId + task.Request);
            }
            catch (Exception e)
            {
                new Task(() =>
                {
                    SendToUser(Secrets.OwnerChatId, "222Ошибка Парсера " + GetType().Name + " EXCEPTION:" + e.Message
                                                    + " userId=" + userId +
                                                    " task.Request=" + task.Request +
                                                    " task.TaskId=" + task.TaskId +
                                                    " task.Data=" + task.Data, bot, cancellationToken);
                }).Start();

                await SendToUser(Secrets.OwnerChatId, "Ошибка Парсера " + GetType().Name + " EXCEPTION:" + e.Message
                                                      + " userId=" + userId +
                                                      " task.Request=" + task.Request +
                                                      " task.TaskId=" + task.TaskId +
                                                      " task.Data=" + task.Data, bot, cancellationToken);

                await SendToUser(userId,
                    GetType().Name + " Не смог обработать запрос, обновите его или обратитесь к администратору",
                    bot, cancellationToken);

                onDeleteWithParsing(userId, task.TaskId);
            }
        }

        public string AcceptLink => "https://www.kufar.by/";
    }
}
