using System;
using System.Threading.Tasks;
using MySeenParserBot.TelegramBots.MySeenParserBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace stdcontrols.TelegramBots.MySeenParserBot.Commands
{
    public class ListCommand : CommandBase
    {
        public override string Name => @"/List";
        public override string Description => @"- для просмотра доступных приложений";
        public override bool IsOnlyForOwner => true;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var ret = bot.GetTasks();

            await botClient.SendTextMessageAsync(message.Chat.Id,
                (string.IsNullOrEmpty(ret) ? "Пусто" :("Задача / Активно / ссылка " + Environment.NewLine + ret))
                //С разметкой бывает беда беда, будет нужна, включу где надо, ParseMode.Markdown
                );
        }
    }
}