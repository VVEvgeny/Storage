using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class StopAllCommand : CommandBase
    {
        public override string Name => @"/StopAll";
        public override string Description => @"- Удалить все проверяемые ссылки";
        public override bool IsOnlyForOwner => false;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot botClient)
        {
            botClient.BotTasks.RemoveAllMyTasks(message.Chat.Id);
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа остановлена", botClient.GetCancellationToken());
        }
    }
}