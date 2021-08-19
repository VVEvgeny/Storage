using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class StopAllTasksCommand : CommandBase
    {
        public override string Name => @"/StopAllTasks";
        public override string Description => @"- Удалить все проверяемые ссылки для всех";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot botClient)
        {
            botClient.BotTasks.RemoveAllTasks();
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа остановлена", botClient.GetCancellationToken());
        }
    }
}