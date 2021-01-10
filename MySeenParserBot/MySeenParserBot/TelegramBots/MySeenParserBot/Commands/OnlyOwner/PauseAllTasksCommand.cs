using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class PauseAllTasksCommand : CommandBase
    {
        public override string Name => @"/PauseAllTasks";
        public override string Description => @"- Приостановить все проверяемые ссылки для всех";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            bot.BotTasks.PauseAllTasks();
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа остановлена");
        }
    }
}