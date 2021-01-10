using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class ListAllCommand : CommandBase
    {
        public override string Name => @"/ListAll";
        public override string Description => "- для просмотра всех ссылок всех пользователей";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var ret = bot.BotTasks.GetAllTasks();

            await botClient.SendTextMessageAsync(message.Chat.Id,
                (string.IsNullOrEmpty(ret)
                    ? "Пусто"
                    : ("Пользователь / Задача / Активно / Ссылка " + Environment.NewLine + ret)));
        }
    }
}