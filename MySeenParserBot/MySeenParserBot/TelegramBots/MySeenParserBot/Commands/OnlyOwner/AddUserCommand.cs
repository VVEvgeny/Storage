using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class AddUserCommand : CommandBase
    {
        public override string Name => @"/AddUser";
        public override string Description => @"ID - Добавить пользователя для разрешения пользования ботом";
        public override bool IsOnlyForOwner => true;

        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var userid = Convert.ToInt64(message.Text.Remove(0, Name.Length));
            if (userid != 0)
            {
                lock (Bot.AcceptedUsers)
                {
                    Bot.AcceptedUsers.Add(userid);
                }

                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Успешно добавлено");
            }
            else
            {

                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Ошибка добавления пользователя");
            }
        }
    }
}