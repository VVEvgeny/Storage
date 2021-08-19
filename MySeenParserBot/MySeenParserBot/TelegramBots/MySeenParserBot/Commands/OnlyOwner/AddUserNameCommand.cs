using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner
{
    public class AddUserNameCommand : CommandBase
    {
        public override string Name => @"/AddUserName";
        public override string Description => @"ID NAME - Добавить имя для пользователя";
        public override bool IsOnlyForOwner => true;

        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot botClient)
        {
            //addusername 123123123 name
            try
            {
                var userid = Convert.ToInt64(message.Text.Split(' ')[1]);
                var userName = message.Text.Split(' ')[2];
                if (userid != 0)
                {
                    bool isNew = true;
                    lock (Bot.KnownUserNames)
                    {
                        if (Bot.KnownUserNames.ContainsKey(userid))
                        {
                            isNew = false;
                            Bot.KnownUserNames[userid] = userName;
                        }
                        else
                        {
                            Bot.KnownUserNames.Add(userid, userName);
                        }
                    }

                    await botClient.SendTextMessageAsync(message.Chat.Id,
                        "Успешно " + (isNew ? "добавлено" : "обновлено") + " имя", botClient.GetCancellationToken());
                    return;
                }
            }
            catch
            {
                // ignored
            }

            await botClient.SendTextMessageAsync(message.Chat.Id,
                "Ошибка, не смогли добавить/обновить имя", botClient.GetCancellationToken());
        }
    }
}