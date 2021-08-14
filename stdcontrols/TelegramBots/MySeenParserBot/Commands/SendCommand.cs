using System;
using System.Threading.Tasks;
using stdcontrols.TelegramBots.MySeenParserBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class SendCommand : CommandBase
    {
        public override string Name => @"/Send";
        public override string Description => @"ID COMMAND - отправить Задаче команду";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            int id=-1;
            string command="-";
            try
            {
                id = Convert.ToInt32(message.Text.Split(' ')[1]);
                command = message.Text.Split(' ')[2];
            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Ошибка обработки, не разобрал номер задачи(" + id + ")(" + command + ")");
                return;
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, bot.SendToTask(id,command));
        }
    }
}