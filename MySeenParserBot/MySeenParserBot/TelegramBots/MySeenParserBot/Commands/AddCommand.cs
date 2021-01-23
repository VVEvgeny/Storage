using System;
using System.Linq;
using System.Threading.Tasks;
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
            var link = message.Text.Remove(0, Name.Length + 1);

            var parser = Bot.AvailableParsers.FirstOrDefault(p => link.StartsWith(p.AcceptLink));
            if (parser != null)
            {
                //var activeTask = 
                bot.BotTasks.AddTask(message.Chat.Id, link);

                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Успешно добавлено");

                if (message.Chat.Id != Secrets.OwnerChatId)
                {
                    await botClient.SendTextMessageAsync(Secrets.OwnerChatId, BotTasks.GetUserNameIfKnown(message.Chat.Id) + " Добавил ссылку: " + link);
                }

                /*
                 AddTask
                 0 second wait - start!
                new Task(() =>
                    {
                        //AV_BY.ProcessTask(message.Chat.Id, activeTask, botClient, CancellationToken.None, bot.BotTasks.SaveDataProcessTask, bot.BotTasks.OnDeleteWithParsing);
                        p.ProcessTask(message.Chat.Id, activeTask, botClient, CancellationToken.None, bot.BotTasks.SaveDataProcessTask, bot.BotTasks.OnDeleteWithParsing);
                    })
                    .Start();
                */
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,
                    "Ошибка, пока поддерживаю только:" + Environment.NewLine + Bot.AvailableParsers.Aggregate("", (current, p) => current + (p.AcceptLink + Environment.NewLine))
                    + link);
            }
        }

    }
}