using System.Threading.Tasks;
using MySeenParserBot.TelegramBots.MySeenParserBot;
using MySeenParserBot.TelegramBots.MySeenParserBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace stdcontrols.TelegramBots.MySeenParserBot.Commands
{
    public class StartCommand : CommandBase
    {
        public override string Name => @"/Start";
        public override string Description => "";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId,Bot.AvailableCommands(chatId == Secrets.OwnerChatId));
        }
    }
}