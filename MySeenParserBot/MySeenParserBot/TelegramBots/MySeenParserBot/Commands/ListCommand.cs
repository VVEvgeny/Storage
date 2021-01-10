using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public class ListCommand : CommandBase
    {
        public override string Name => @"/List";
        public override string Description => @"- для просмотра своих ссылок";
        public override bool IsOnlyForOwner => false;
        public override MessageType MessageType => MessageType.Text;

        public override async Task Execute(Message message, Bot bot, TelegramBotClient botClient)
        {
            var ret = bot.BotTasks.GetMyTasks(message.Chat.Id);

            await botClient.SendTextMessageAsync(message.Chat.Id,
                (string.IsNullOrEmpty(ret) ? "Пусто" :("Задача / Активно / ссылка " + Environment.NewLine + ret))
                //С разметкой бывает беда беда, будет нужна, включу где надо, ParseMode.Markdown
                );
        }
    }
}