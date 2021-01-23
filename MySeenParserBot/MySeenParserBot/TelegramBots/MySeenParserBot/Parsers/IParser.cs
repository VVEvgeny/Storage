using System.Threading;
using Telegram.Bot;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public interface IParser
    {
        void ProcessTask(long userId, BotTasks.BotTask task,
            TelegramBotClient bot, CancellationToken cancellationToken,
            BotTasks.SaveDataProcessTaskDelegate saveDataProcessTask,
            BotTasks.OnDeleteWithParsingDelegate onDeleteWithParsing);

        string AcceptLink { get; }
    }
}
