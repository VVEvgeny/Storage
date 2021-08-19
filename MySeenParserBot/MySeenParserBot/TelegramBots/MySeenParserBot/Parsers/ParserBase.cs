using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public abstract class ParserBase: IParser
    {
        public static readonly List<ParserBase> Parsers = new List<ParserBase>();

        protected ParserBase()
        {
            Parsers.Add(this);
        }

        public delegate string NormalizeDeligate(string message);
        private readonly Dictionary<long, Queue<string>> _messageCache = new Dictionary<long, Queue<string>>();
        public Task<Telegram.Bot.Types.Message> SendToUser(long userId, string data, Bot bot, CancellationToken cancellationToken, NormalizeDeligate normalize = null)
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

            return bot.SendTextMessageAsync(userId, data, cancellationToken);
        }

        public abstract void ProcessTask(long userId, BotTasks.BotTask task, Bot bot,
            CancellationToken cancellationToken,
            BotTasks.SaveDataProcessTaskDelegate saveDataProcessTask,
            BotTasks.OnDeleteWithParsingDelegate onDeleteWithParsing);

        public abstract string AcceptLink { get; }
    }
}
