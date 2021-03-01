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

        public static class Markup
        {
            public static string List = "a .k-Vopm-74aea";
            public static string Name = "h3 .k-VoRr-e2203";
            public static string Price = "span .k-VleK-99b72";
            public static string Location = "span .k-VaOA-c8871";
            public static string LastUpdate = "div .k-VJOZ-1a3f3";

            public static string ToStringNames()
            {
                return "List:" + List + " Name:" + Name + " Price:" + Price + " Location:" + Location + " LastUpdate:" +
                       LastUpdate;
            }
        }
        private static void ReloadMarkup(HtmlDocument doc)
        {
            try
            {
                //list
                //document.getElementsByTagName('article')[0].getElementsByTagName('a')[0].className;
                var article = doc.QuerySelector("article");
                //richTextBox1.Text += "article=" + article.GetClassList()[0] + Environment.NewLine;
                var a = article.QuerySelector("a");
                Markup.List = "a ." + a.GetClassList()[0];

                //name
                //document.getElementsByTagName('article')[0].getElementsByTagName('a')[0].getElementsByTagName('h3')[0].className;
                Markup.Name = "h3 ." + a.QuerySelector("h3").GetClassList()[0];

                //price
                //document.getElementsByTagName('article')[0].getElementsByTagName('a')[0].getElementsByTagName('span')[1].className;
                Markup.Price = "span ." + a.QuerySelectorAll("span")[1].GetClassList()[0];

                //location
                //document.getElementsByTagName('article')[0].getElementsByTagName('a')[0].getElementsByTagName('span')[2].className;
                Markup.Location = "span ." + a.QuerySelectorAll("span")[2].GetClassList()[0];

                //update
                //document.getElementsByTagName('article')[0].getElementsByTagName('a')[0].getElementsByTagName('div')[22].className;
                /*
                try
                {
                    Markup.LastUpdate = "div ." + a.QuerySelectorAll("div")[22].GetClassList()[0];
                }
                catch (Exception e)
                {
                    try
                    {
                        Markup.LastUpdate = "div ." + a.QuerySelectorAll("div")[19].GetClassList()[0];
                    }
                    catch (Exception ee)
                    {
                        //richTextBox1.Text += "LastUpdate EXCEPTION e=" + e.Message + Environment.NewLine;
                    }
                }
                */

            }
            catch (Exception e)
            {
                //richTextBox1.Text += "EXCEPTION e=" + e.Message + Environment.NewLine;
            }
        }

        private static CacheWebItemStorage<Item> Cache = new CacheWebItemStorage<Item>();

        private static List<Item> GetItemsFromWeb(string url, TelegramBotClient bot,
            CancellationToken cancellationToken)
        {
            if (Cache.ContainsActual(url))
                return Cache.Get(url);

            var items = new List<Item>();

            // From Web
            //var url = "https://www.kufar.by/listings?cat=16010&cct=11&rgn=all";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var nodes = doc.TryGetNodeAll(Markup.List);
            if (nodes == null)
            {
                new Task(() =>
                {
                    bot.SendTextMessageAsync(Secrets.OwnerChatId,
                        "Ошибка парсера Куфар, похоже опять изменилась разметка!, пробую перезагрузить !",
                        cancellationToken: cancellationToken);
                }).Start();

                ReloadMarkup(doc);
                nodes = doc.TryGetNodeAll(Markup.List);
                if (nodes == null)
                {
                    new Task(() =>
                    {
                        bot.SendTextMessageAsync(Secrets.OwnerChatId,
                            "Ошибка парсера Куфар, похоже опять изменилась разметка!, не смогли разобрать...",
                            cancellationToken: cancellationToken);
                    }).Start();
                    return items;
                }

                new Task(() =>
                {
                    bot.SendTextMessageAsync(Secrets.OwnerChatId,
                        "Успешно перезагружено, "+ Markup.ToStringNames() + " nodes.Count=" + nodes.Count,
                        cancellationToken: cancellationToken);
                }).Start();

                if (nodes.Count > 0)
                {
                    new Task(() =>
                    {
                        bot.SendTextMessageAsync(Secrets.OwnerChatId,
                            "Первая перезагруженная объявка: " + Environment.NewLine
                                                               + ShowItem(new Item
                                                               {
                                                                   Name = nodes[0].TryGetNode(Markup.Name)?.InnerText,
                                                                   Image = nodes[0].QuerySelector("img")
                                                                       ?.Attributes["data-src"]?.Value,
                                                                   Info = "",
                                                                   Price = nodes[0].TryGetNode(Markup.Price)?.InnerText,
                                                                   Location = nodes[0].TryGetNode(Markup.Location)
                                                                       ?.InnerText,
                                                                   //LastUpdate = nodes[0].TryGetNode(Markup.LastUpdate)?.InnerText,
                                                                   Link = nodes[0].Attributes["href"]?.Value
                                                               }),
                            cancellationToken: cancellationToken);
                    }).Start();
                }
            }

            foreach (var node in nodes)
            {
                var item = new Item
                {
                    /*
                    Name = node.TryGetNode("h3 .k-fxbJ-16e40", "h3 .k-pmvA-ebdbb", "h3 .k-oNeS-2bc8b",
                        "h3 .k-gKhZ-c7dad", "h3 .k-VoRr-e2203")?.InnerText,
                    //Name = node.QuerySelector("div .listing-item__about")?.QuerySelector("h3 a span")?.InnerText?.Replace("<!-- -->", ""),
                    Image = node.QuerySelector("img")?.Attributes["data-src"]?.Value,
                    Info = "",
                    Price = node.TryGetNode("span .k-beVe-c6572", "span .k-hNKj-19b8b", "span .k-eRcp-1d52b",
                        "span .k-gNUS-96512", "span .k-VleK-99b72")?.InnerText,
                    Location = node.TryGetNode("span .k-fwEX-8b3f6", "span .k-pYsu-eb441", "span .k-oOUx-d4470",
                        "span .k-geUH-549bc", "span .k-VaOA-c8871")?.InnerText,
                    LastUpdate = node.TryGetNode("div .k-fFtb-c33c6", "div .k-pxsl-541ef", "div .k-oFUG-3b966",
                        "div .k-gMUw-ffda9", "div .k-VJOZ-1a3f3")?.InnerText,
                    Link = node.Attributes["href"]?.Value
                    */
                    Name = node.TryGetNode(Markup.Name)?.InnerText,
                    Image = node.QuerySelector("img")?.Attributes["data-src"]?.Value,
                    Info = "",
                    Price = node.TryGetNode(Markup.Price)?.InnerText,
                    Location = node.TryGetNode(Markup.Location)?.InnerText,
                    //LastUpdate = node.TryGetNode(Markup.LastUpdate)?.InnerText,
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

                var cw = GetItemsFromWeb(task.Request, bot, cancellationToken);
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
