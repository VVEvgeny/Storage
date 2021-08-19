using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public class AV_BY: ParserBase
    {
        [Serializable]
        private class Car
        {
            public string Name;
            public string Image;
            public string Info;
            public string Link;
            public string Location;
            public string LastUpdate;
            public string Price;
        }

        private static CacheWebItemStorage<Car> Cache = new CacheWebItemStorage<Car>();

        private static List<Car> GetCarsFromWeb(string url)
        {
            if (Cache.ContainsActual(url))
            {
                //if (Cache.Get(url) == null) DebugGlobal.Write(nameof(Cache));
                return Cache.Get(url);
            }

            var cars = new List<Car>();

            // From Web
            //var url = "https://cars.av.by/filter?condition[0]=3&condition[1]=4&sort=4";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var nodes = doc.QuerySelectorAll("div .listing-item");

            foreach (var node in nodes)
            {
                var car = new Car
                {
                    //Name = node.QuerySelector("img")?.Attributes["alt"]?.Value,
                    Name = node.QuerySelector("div .listing-item__about")?.QuerySelector("h3 a span")?.InnerText?.Replace("<!-- -->", ""),
                    Image = node.QuerySelector("img")?.Attributes["data-src"]?.Value,
                    Info = node.QuerySelector("div .listing-item__params")?.InnerText?.Replace("<!-- -->", ""),
                    Price = node.QuerySelector("div .listing-item__price")?.InnerText +
                            node.QuerySelector("div .listing-item__priceusd")?.InnerText,
                    Location = node.QuerySelector("div .listing-item__location")?.InnerText,
                    LastUpdate = node.QuerySelector("div .listing-item__date")?.InnerText,
                    Link = node.QuerySelector("a .listing-item__link")?.Attributes["href"]?.Value
                };


                if (!string.IsNullOrEmpty(car.Link))
                {
                    var myUri = new Uri(url);
                    car.Link = myUri.ToString().Replace(myUri.PathAndQuery, "") + car.Link;
                }

                //ShowCar(car);

                cars.Add(car);
            }

            Cache.AddOrUpdate(url, cars);

            return cars;
        }
        private class CarDiff
        {
            public Car InStorage;
            public Car InWeb;

            public bool NewForThisTime;

            //what changed
            public bool Name;
            public bool Image;
            public bool Info;
            public bool Location;
            public bool LastUpdate = false;
            public bool Price;
        }

        private static bool ComparePrice(string priceWas, string priceNew)
        {
            //2 181 р.≈ 850 $
            //Цена Было:32 325 р.≈ 12 600 $ Стало:32 492 р.≈ 12 600 $
            try
            {
                return priceWas.Split('≈')[0] == priceNew.Split('≈')[0] ||
                       priceWas.Split('≈')[1] == priceNew.Split('≈')[1];
            }
            catch
            {
                // ignored
            }

            return priceWas == priceNew;
        }
        private static List<CarDiff> CompareCars(List<Car> fromStorage, List<Car> fromWeb)
        {
            var cd = new List<CarDiff>();
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
                        cd.Add(new CarDiff { InWeb = cw, NewForThisTime = true });
                }
                else
                {
                    hit = true;
                    if (cs.Name != cw.Name ||
                        cs.Image != cw.Image ||
                        cs.Info != cw.Info ||
                        cs.Location != cw.Location ||
                        //cs.LastUpdate != cw.LastUpdate ||
                        //cs.Price != cw.Price
                        !ComparePrice(cs.Price,cw.Price)
                        )
                    {
                        cd.Add(new CarDiff
                        {
                            InWeb = cw,
                            InStorage = cs,
                            NewForThisTime = false,
                            Name = cs.Name != cw.Name,
                            Image = cs.Image != cw.Image,
                            Info = cs.Info != cw.Info,
                            Location = cs.Location != cw.Location,
                            //LastUpdate = cs.LastUpdate != cw.LastUpdate,
                            Price = !ComparePrice(cs.Price, cw.Price)
                        });
                    }
                }
            }
            return cd;
        }
        private static string ShowCar(Car car)
        {
            var ret = "";
            ret += "==========ДОБАВЛЕНО===========" + Environment.NewLine;
            ret += "Название: " + car.Name + Environment.NewLine;
            ret += "Изображение: " + car.Image + Environment.NewLine;
            ret += "Хар-ки: " + car.Info + Environment.NewLine;
            ret += "Цена: " + car.Price + Environment.NewLine;
            ret += "Где: " + car.Location + Environment.NewLine;
            ret += "Обновлено: " + car.LastUpdate + Environment.NewLine;
            ret += "Ссылка: " + car.Link + Environment.NewLine;
            return ret;
        }
        private static string ShowCarChange(CarDiff card)
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

        private static string RemoveAddInfo(string str)
        {
            if (str.Contains("Обновлений:"))
            {
                try
                {
                    var beginInfo = str.IndexOf("=", StringComparison.Ordinal);
                    if (beginInfo > 0)
                    {
                        str = str.Substring(beginInfo);
                    }
                    var updated = str.IndexOf("Обновлено:", StringComparison.Ordinal);
                    var link = str.IndexOf("Ссылка:", StringComparison.Ordinal);
                    if (updated > 0 && link > 0 && link - updated > 0)
                    {
                        str = str.Remove(updated, link - updated);
                    }

                    return str;
                }
                catch
                {
                    return str;
                }
            }
            return str;
        }

        //1й раз не показываем этот огромный список!
        private readonly HashSet<string> _running = new HashSet<string>();

        public override async void ProcessTask(long userId, BotTasks.BotTask task,
            Bot bot, CancellationToken cancellationToken, BotTasks.SaveDataProcessTaskDelegate saveDataProcessTask, BotTasks.OnDeleteWithParsingDelegate onDeleteWithParsing)
        {
            try
            {
                var silentBecauseFirstRun = !_running.Contains(userId + task.Request);

                var cw = GetCarsFromWeb(task.Request);
                if (!silentBecauseFirstRun)
                {
                    var cs = string.IsNullOrEmpty(task.Data)
                        ? null
                        : JsonConvert.DeserializeObject<List<Car>>(task.Data);
                    
                    if (cs == null || cs.Count == 0)
                    {
                        //first run? storage is empty? just save and show All

                        foreach (var c in cw)
                        {
                            await SendToUser(userId, ShowCar(c), bot, cancellationToken, RemoveAddInfo);
                        }
                    }
                    else
                    {
                        var cd = CompareCars(cs, cw);

                        if (cd.Count != 0)
                        {
                            /*await bot.SendTextMessageAsync(userId, "Обновлений:" + cd.Count,
                                cancellationToken: cancellationToken);*/
                            string addData = "Обновлений:" + cd.Count + Environment.NewLine;

                            foreach (var card in cd)
                            {
                                if (card.NewForThisTime)
                                {
                                    await SendToUser(userId, addData + ShowCar(card.InWeb), bot, cancellationToken,
                                        RemoveAddInfo);
                                }
                                else
                                {
                                    await SendToUser(userId, addData + ShowCarChange(card), bot, cancellationToken,
                                        RemoveAddInfo);
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
                //DebugGlobal.Write("Ошибка Парсера " + GetType().Name + " EXCEPTION:" + e.Message);

                await SendToUser(userId,
                    GetType().Name + " Не смог обработать запрос, обновите его или обратитесь к администратору",
                    bot, cancellationToken);

                await SendToUser(Secrets.OwnerChatId, "Ошибка Парсера " + GetType().Name + " EXCEPTION:" + e.Message
                                                      + " userId=" + userId +
                                                      " task.Request=" + task.Request +
                                                      " task.TaskId=" + task.TaskId +
                                                      " task.Data=" + task.Data, bot, cancellationToken);

                onDeleteWithParsing(userId, task.TaskId);
            }
        }

        public override string AcceptLink => "https://cars.av.by/";
    }
}