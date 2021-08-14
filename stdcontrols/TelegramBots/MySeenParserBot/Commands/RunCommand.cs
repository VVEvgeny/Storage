using System;
using System.Threading.Tasks;
using stdcontrols.TelegramBots.MySeenParserBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class RunCommand : CommandBase
    {
        public override string Name => @"/Run";
        public override string Description => @"ID - Запускаем задачу";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            int id;
            try
            {
                id = Convert.ToInt32(message.Text.Remove(0, Name.Length));
            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка удаления, не разобрал номер задачи");
                return;
            }

            await botClient.SendTextMessageAsync(message.Chat.Id, bot.RunTask(id));
        }
    }
}