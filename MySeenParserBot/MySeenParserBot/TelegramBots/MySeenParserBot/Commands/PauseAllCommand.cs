using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class PauseAllCommand : CommandBase
    {
        public override string Name => @"/PauseAll";
        public override string Description => @"- Приостановить все проверяемые ссылки";
        public override bool IsOnlyForOwner => false;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            bot.BotTasks.PauseAllMyTasks(message.Chat.Id);
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа приостановлена");
        }
    }
}