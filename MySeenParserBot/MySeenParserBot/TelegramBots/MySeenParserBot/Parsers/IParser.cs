using System.Threading;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Parsers
{
    public interface IParser
    {
        void ProcessTask(long userId, BotTasks.BotTask task,
            Bot bot, CancellationToken cancellationToken,
            BotTasks.SaveDataProcessTaskDelegate saveDataProcessTask,
            BotTasks.OnDeleteWithParsingDelegate onDeleteWithParsing);

        string AcceptLink { get; }
    }
}
