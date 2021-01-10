using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class UnPauseCommand : CommandBase
    {
        public override string Name => @"/UnPause";
        public override string Description => @"ID - Возобновить проверку ID ссылки";
        public override bool IsOnlyForOwner => false;
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

            if (bot.BotTasks.UnPauseMyTask(message.Chat.Id, id))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,"Успешно возобновлено");
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ошибка возобновления");
            }
        }
    }
}