using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySeenParserBot.TelegramBots.MySeenParserBot;
using MySeenParserBot.TelegramBots.MySeenParserBot.Commands;
using stdcontrols.TelegramBots.MySeenParserBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace stdcontrols.TelegramBots.MySeenParserBot
{
    public class Bot
    {
        private bool _enableInputRedirectToBot = true;
        public Bot()
        {
            _commandsList = new List<CommandBase>
            {
                //help
                new StartCommand(),
                //
                new RunCommand(),
                new StopCommand(),
                new SendCommand(),
                new ListCommand()
            };
        }

        private CancellationTokenSource _cancelTokenSource;

        public static HashSet<long> AcceptedUsers = new HashSet<long>()
        {
            Secrets.OwnerChatId
        };

        public string GetTasks()
        {
            var sb = new StringBuilder();
            //Задача / Активно / ссылка
            lock (_tasks)
            {
                foreach (var t in _tasks)
                {
                    sb.Append(t.Id + " " + t.Status + " " + t.Name + Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        public string RunTask(int id)
        {
            lock (_tasks)
            {
                if (id > _tasks.Length)
                {
                    return "Столько задач НЕТУ";
                }
            }

            string ret = "";
            lock (_tasks)
            {
                ret = _tasks[id-1].Start();
            }

            return ret;
        }
        public string StopTask(int id)
        {
            lock (_tasks)
            {
                if (id > _tasks.Length)
                {
                    return "Столько задач НЕТУ";
                }
            }

            string ret = "";
            lock (_tasks)
            {
                ret = _tasks[id-1].Stop();
            }

            return ret;
        }

        private static List<CommandBase> _commandsList;
        public static string AvailableCommands(bool forOwner)
        {
            string ret = "Доступные команды бота:" + Environment.NewLine;

            foreach (var c in Commands)
            {
                if (!c.IsOnlyForOwner || forOwner)
                {
                    ret += c.Name + " " + c.Description + (c.IsOnlyForOwner ? " (Только владельцу)" : "") +
                           Environment.NewLine;
                }
            }

            return ret;
        }

        public string SendToTask(int id, string message)
        {
            lock (_tasks)
            {
                if (id > _tasks.Length)
                {
                    return "Столько задач НЕТУ";
                }
            }

            string ret = "";
            lock (_tasks)
            {
                ret = _tasks[id - 1].Send(message);
            }

            return ret;
        }

        public static IReadOnlyList<CommandBase> Commands => _commandsList.AsReadOnly();

        public delegate void WriteDebugInfo(string message);

        private WriteDebugInfo _writeDebugInfo;
        private WriteDebugInfo _updateServiceStatus;

        private Tasks[] _tasks;

        public void SetDebugHook(WriteDebugInfo writeDebugInfo)
        {
            _writeDebugInfo = writeDebugInfo;
        }

        public void TaskStatusChanged(string s)
        {
            _botClient.SendTextMessageAsync(Secrets.OwnerChatId, s);
        }

        public void OutputChanged(string s)
        {
            if(_enableInputRedirectToBot)
                _botClient.SendTextMessageAsync(Secrets.OwnerChatId, s);
        }

        public void SetTasks(Tasks[] tasks)
        {
            lock (tasks)
            {
                _tasks = tasks;

                foreach (var t in tasks)
                {
                    t.StatusChanged += TaskStatusChanged;
                }
            }
        }
        public void SetServiceStatusHook(WriteDebugInfo updateServiceStatus)
        {
            _updateServiceStatus = updateServiceStatus;
        }

        public bool StartAsService()
        {
            if (_cancelTokenSource != null)
                return false;

            _cancelTokenSource = new CancellationTokenSource();

            try
            {
                new Task(() => { Service(_cancelTokenSource.Token); }).Start();
            }
            catch (Exception e)
            {
                _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "TASK Start EXCEPTION e=" + e.Message);
            }

            return true;
        }

        public bool StopService()
        {
            if (_cancelTokenSource == null)
                return false;

            _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "I'm offline on: " + Environment.MachineName);

            _cancelTokenSource.Cancel();
            _cancelTokenSource = null;
            return true;
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null)
            {
                return;
            }

            if (!AcceptedUsers.Contains(message.Chat.Id))
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Это приватный бот, необходимо получить на него разрешение");

                await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "Кто-то ломиться в бота без разрешения: " + message.Chat.Id);

                return;
            }

            bool processed = false;
            foreach (var command in Commands)
            {
                if (command.IsItMyCommand(message))
                {
                    try
                    {
                        if (command.IsOnlyForOwner && message.Chat.Id != Secrets.OwnerChatId)
                        {
                            await _botClient.SendTextMessageAsync(message.Chat.Id, "Это действие доступно только владельцу");

                            await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "Кто-то ломиться в бота командой владельца: " + message.Chat.Id + " command=" + message.Text);

                            return;
                        }

                        await command.Execute(message, this, _botClient);
                    }
                    catch (Exception e)
                    {
                        await _botClient.SendTextMessageAsync(message.Chat.Id, GetType().Name + " Не смог обработать запрос, обновите его или обратитесь к администратору");

                        await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "Ошибка контролера EXCEPTION:" + e.Message
                                                                                                             + " userId=" + message.Chat.Id
                                                                                                             + " message.Text=" + message.Text
                                                                                                             + " command=" + command.Name
                                                                                                   );
                    }
                    processed = true;
                    break;
                }
            }

            if (!processed)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Команда не распознана, для начала работы вызовите /start");
            }
        }
        //service
        private TelegramBotClient _botClient;

        private async void Service(CancellationToken cancellationToken)
        {
            _writeDebugInfo?.Invoke("Service started! ");

            _botClient = new TelegramBotClient(Secrets.MySeenParserBotToken);
            _botClient.OnMessage += BotOnMessageReceived;
            _botClient.OnMessageEdited += BotOnMessageReceived;
            _botClient.StartReceiving(cancellationToken: cancellationToken);

            await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "I'm online from:" + Environment.MachineName,
                cancellationToken: cancellationToken);

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _writeDebugInfo?.Invoke("Service canceled!");
                    try
                    {
                        _botClient.StopReceiving();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    return;
                }

                Thread.Sleep(1000);
                //_writeDebugInfo?.Invoke("Service after sleep");
            }
        }
    }
}
