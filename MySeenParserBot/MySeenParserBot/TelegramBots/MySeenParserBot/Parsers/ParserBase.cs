using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public abstract class ParserBase
    {
        public delegate string NormalizeDeligate(string message);
        private readonly Dictionary<long, Queue<string>> MessageCache = new Dictionary<long, Queue<string>>();
        public Task<Telegram.Bot.Types.Message> SendToUser(long userId, string data, ITelegramBotClient bot, CancellationToken cancellationToken, NormalizeDeligate Normalize = null)
        {
            if (Normalize != null)
            {
                if (MessageCache.ContainsKey(userId))
                {
                    var queue = MessageCache[userId];
                    var dataNormalized = Normalize(data);
                    if (queue.Any(q => Normalize(q) == dataNormalized))
                    {
                        //cached and already sended
                        return null;
                    }

                    if (queue.Count >= 20)
                        queue.Dequeue();

                    queue.Enqueue(data);
                }
                else
                {
                    MessageCache.Add(userId, new Queue<string>());
                    MessageCache[userId].Enqueue(data);
                }
            }

            return bot.SendTextMessageAsync(userId, data, cancellationToken: cancellationToken);
        }
    }
}
