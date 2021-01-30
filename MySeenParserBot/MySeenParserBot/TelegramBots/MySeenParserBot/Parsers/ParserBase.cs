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
        private readonly Dictionary<long, Queue<string>> _messageCache = new Dictionary<long, Queue<string>>();
        public Task<Telegram.Bot.Types.Message> SendToUser(long userId, string data, ITelegramBotClient bot, CancellationToken cancellationToken, NormalizeDeligate normalize = null)
        {
            if (normalize != null)
            {
                if (_messageCache.ContainsKey(userId))
                {
                    var queue = _messageCache[userId];
                    var dataNormalized = normalize(data);
                    if (queue.Any(q => normalize(q) == dataNormalized))
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
                    _messageCache.Add(userId, new Queue<string>());
                    _messageCache[userId].Enqueue(data);
                }
            }

            return bot.SendTextMessageAsync(userId, data, cancellationToken: cancellationToken);
        }
    }
}
