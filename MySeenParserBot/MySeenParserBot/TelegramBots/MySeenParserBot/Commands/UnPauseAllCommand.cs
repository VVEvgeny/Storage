using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class UnPauseAllCommand : CommandBase
    {
        public override string Name => @"/UnPauseAll";
        public override string Description => @"- Возобновить все проверяемые ссылки";
        public override bool IsOnlyForOwner => false;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot botClient)
        {
            botClient.BotTasks.UnPauseAllMyTasks(message.Chat.Id);
            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Работа возобновлена", botClient.GetCancellationToken());
        }
    }
}