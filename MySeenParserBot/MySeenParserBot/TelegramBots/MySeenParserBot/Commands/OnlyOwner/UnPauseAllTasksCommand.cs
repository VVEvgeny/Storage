using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class UnPauseAllTasksCommand : CommandBase
    {
        public override string Name => @"/UnPauseAllTasks";
        public override string Description => @"- Возобновить все проверяемые ссылки для всех";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            bot.BotTasks.UnPauseAllTasks();
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа возобновлена");
        }
    }
}