using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MySeenParserBot.TelegramBots.MySeenParserBot.Commands
{
    public abstract class CommandBase
    {
        public static readonly List<CommandBase> Commands = new List<CommandBase>();

        protected CommandBase()
        {
            Commands.Add(this);
        }

        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract bool IsOnlyForOwner { get; }
        public abstract Task Execute(Message message, Bot client);
        public abstract Telegram.Bot.Types.Enums.MessageType MessageType { get; }

        public bool IsItMyCommand(Message message)
        {
            if (MessageType != message.Type)
                return false;

            return message.Text?.ToLower().IndexOf(Name.ToLower(), StringComparison.Ordinal) == 0;
        }
    }
}