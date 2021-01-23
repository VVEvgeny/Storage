using System;
using System.Collections.Generic;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public class CachedWebItem<T>
    {
        public List<T> Items;
        public DateTime Time;

        public CachedWebItem(DateTime time, List<T> items)
        {
            Time = time;
            Items = items;
        }
    }
    public class CacheWebItemStorage<T> : Dictionary<string, CachedWebItem<T>>
    {
        public bool ContainsActual(string url)
        {
            if (ContainsKey(url))
            {
                if ((DateTime.Now - this[url].Time).TotalSeconds < 58)
                {
                    return true;
                }
            }

            return false;
        }

        public List<T> Get(string url)
        {
            if (ContainsKey(url))
            {
                if ((DateTime.Now - this[url].Time).TotalSeconds < 58)
                {
                    return this[url].Items;
                }
            }

            return null;
        }

        public void AddOrUpdate(string url, List<T> items)
        {
            if (ContainsKey(url))
            {
                //update
                this[url].Time = DateTime.Now;
                this[url].Items = items;
            }
            else
            {
                Add(url, new CachedWebItem<T>(DateTime.Now, items));
            }
        }
    }
}
