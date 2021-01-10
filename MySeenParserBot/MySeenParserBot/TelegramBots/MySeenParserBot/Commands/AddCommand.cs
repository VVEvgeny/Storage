using System.Threading;
using System.Threading.Tasks;
using MySeenParserBot.TelegramBots.MySeenParserBot.Parsers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class AddCommand : CommandBase
    {
        public override string Name => @"/Add";
        public override string Description => @"LINK - Добавить ссылку";
        public override bool IsOnlyForOwner => false;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var link = message.Text.Remove(0, Name.Length);

            if (link.StartsWith("https://cars.av.by/"))
            {
                var activeTask = bot.BotTasks.AddTask(message.Chat.Id, link);

                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Успешно добавлено");

                new Task(() => { AV_BY.ProcessTask(message.Chat.Id, activeTask, botClient, CancellationToken.None, bot.BotTasks.SaveDataProcessTask, bot.BotTasks.OnDeleteWithParsing); })
                    .Start();
            }
            else
            {

                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Ошибка, пока поддерживаю только https://cars.av.by/");
            }
        }

    }
}