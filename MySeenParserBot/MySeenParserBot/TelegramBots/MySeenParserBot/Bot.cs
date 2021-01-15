using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MySeenParserBot.TelegramBots.MySeenParserBot.Commands;
using MySeenParserBot.TelegramBots.MySeenParserBot.Commands.OnlyOwner;
using MySeenParserBot.TelegramBots.MySeenParserBot.Parsers;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MySeenParserBot.TelegramBots.MySeenParserBot
{
    public class Bot
    {
        public Bot()
        {
            BotTasks = new BotTasks();

            _commandsList = new List<CommandBase>
            {
                //only owner
                new AddUserNameCommand(),
                new AddUserCommand(),
                new ListAllCommand(),//+status service
                new UnPauseAllTasksCommand(),
                new PauseAllTasksCommand(),
                new StopAllTasksCommand(),
                //all
                new StartCommand(),
                new AddCommand(),
                new UnPauseAllCommand(),
                new UnPauseCommand(),
                new PauseAllCommand(),
                new PauseCommand(),
                new StopAllCommand(),
                new StopCommand(),
                new ListCommand()
            };
        }



        private CancellationTokenSource _cancelTokenSource;

        public static HashSet<long> AcceptedUsers = new HashSet<long>()
        {
            Secrets.OwnerChatId
        };

        public static Dictionary<long, string> KnownUserNames = new Dictionary<long, string>()
        {
            { Secrets.OwnerChatId, Secrets.OwnerChatName },
            { Secrets.FrineChatId1, Secrets.FrineChatName1 },
            { Secrets.FrineChatId2, Secrets.FrineChatName2 }
        };

        public BotTasks BotTasks;

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

        public static IReadOnlyList<CommandBase> Commands => _commandsList.AsReadOnly();

        public delegate void WriteDebugInfo(string message);

        private WriteDebugInfo _writeDebugInfo;
        private WriteDebugInfo _updateServiceStatus;

        public void SetDebugHook(WriteDebugInfo writeDebugInfo)
        {
            _writeDebugInfo = writeDebugInfo;
        }
        public void SetServiceStatusHook(WriteDebugInfo updateServiceStatus)
        {
            _updateServiceStatus = updateServiceStatus;
        }

        private static readonly string AcceptedUsersPath = Environment.CurrentDirectory + "\\" + "AcceptedUsers.txt";

        private void LoadAcceptedUsers()
        {
            lock (AcceptedUsers)
            {
                try
                {
                    AcceptedUsers = JsonConvert.DeserializeObject<HashSet<long>>(File.ReadAllText(AcceptedUsersPath));
                }
                catch (Exception e)
                {
                    AcceptedUsers = new HashSet<long>()
                    {
                        Secrets.OwnerChatId
                    };

                    _writeDebugInfo?.Invoke("LoadAcceptedUsers exception e=" + e.Message);
                }
            }
        }
        private void SaveAcceptedUsers()
        {
            lock (AcceptedUsers)
            {
                var serializer = new JsonSerializer { NullValueHandling = NullValueHandling.Ignore };
                using (var sw = new StreamWriter(AcceptedUsersPath))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, AcceptedUsers);
                    }
                }
            }
        }


        private static readonly string KnownUsersPath = Environment.CurrentDirectory + "\\" + "KnownUsers.txt";

        private void LoadKnownUsers()
        {
            lock (KnownUserNames)
            {
                try
                {
                    KnownUserNames = JsonConvert.DeserializeObject<Dictionary<long, string>>(File.ReadAllText(KnownUsersPath));
                }
                catch (Exception e)
                {
                    KnownUserNames = new Dictionary<long, string>()
                    {
                        { Secrets.OwnerChatId, Secrets.OwnerChatName },
                        { Secrets.FrineChatId1, Secrets.FrineChatName1 },
                        { Secrets.FrineChatId2, Secrets.FrineChatName2 }
                    };

                    _writeDebugInfo?.Invoke("LoadAcceptedUsers exception e=" + e.Message);
                }
            }
        }
        private void SaveKnownUsers()
        {
            lock (KnownUserNames)
            {
                var serializer = new JsonSerializer { NullValueHandling = NullValueHandling.Ignore };
                using (var sw = new StreamWriter(KnownUsersPath))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, KnownUserNames);
                    }
                }
            }
        }




        private static readonly string StoragePath = Environment.CurrentDirectory + "\\" + "storage.txt";
        private void LoadStorage()
        {
            lock (BotTasks.ActiveTasks)
            {
                try
                {
                    //_writeDebugInfo?.Invoke("readFile="+File.ReadAllText(StoragePathTasks));

                    BotTasks.ActiveTasks = JsonConvert.DeserializeObject<Dictionary<long, List<BotTasks.BotTask>>>(File.ReadAllText(StoragePath));

                    int maxTaskId = 0;
                    foreach (var d in BotTasks.ActiveTasks)
                    {
                        foreach (var t in d.Value)
                        {
                            if (t.TaskId > maxTaskId)
                                maxTaskId = t.TaskId;
                        }
                    }

                    BotTasks.SetId(maxTaskId + 1);
                    //_writeDebugInfo?.Invoke(BotTasks.ActiveTasks?.First().Value?.First()?.Request);
                }
                catch (Exception e)
                {
                    BotTasks.ActiveTasks = new Dictionary<long, List<BotTasks.BotTask>>();

                    _writeDebugInfo?.Invoke("LoadStorage exception e=" + e.Message);
                }
            }
        }

        private void SaveStorage()
        {
            lock (BotTasks.ActiveTasks)
            {
                var serializer = new JsonSerializer { NullValueHandling = NullValueHandling.Ignore };
                using (var sw = new StreamWriter(StoragePath))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, BotTasks.ActiveTasks);
                    }
                }
            }
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

            _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "I'm offline");

            SaveStorage();
            SaveAcceptedUsers();
            SaveKnownUsers();

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
                        await _botClient.SendTextMessageAsync(message.Chat.Id, "Не смог обработать запрос, обновите его или обратитесь к администратору");

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

        private const int TimeOutToSaveAll = 60 * 60; //1 per hour
        private void PeriodicalSaveAll()
        {
            SaveAcceptedUsers();
            SaveKnownUsers();
            SaveStorage();
        }
        private async void Service(CancellationToken cancellationToken)
        {
            _writeDebugInfo?.Invoke("Service started!");

            _botClient = new TelegramBotClient(Secrets.MySeenParserBotToken);
            _botClient.OnMessage += BotOnMessageReceived;
            _botClient.OnMessageEdited += BotOnMessageReceived;
            _botClient.StartReceiving(cancellationToken: cancellationToken);

            await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "I'm online", cancellationToken: cancellationToken);

            LoadStorage();
            LoadAcceptedUsers();
            LoadKnownUsers();

            int timeOutToSaveAll = TimeOutToSaveAll;

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

                int totalUsers = 0;
                int activeTasks = 0;
                int totalTasks = 0;

                try
                {
                    lock (BotTasks.ActiveTasks)
                    {
                        foreach (var activeTask in BotTasks.ActiveTasks)
                        {
                            totalUsers++;
                            for (int i = 0; i < activeTask.Value.Count; i++)
                            {
                                totalTasks++;
                                if (activeTask.Value[i].IsActive)
                                {
                                    activeTasks++;
                                    if (activeTask.Value[i].Repeat == 0)
                                    {
                                        activeTask.Value[i].ResetInterval();

                                        var i1 = i;

                                        new Task(() =>
                                        {
                                            AV_BY.ProcessTask(activeTask.Key, activeTask.Value[i1], _botClient,_cancelTokenSource.Token, BotTasks.SaveDataProcessTask, BotTasks.OnDeleteWithParsing);
                                        }).Start();
                                    }
                                    else
                                    {
                                        activeTask.Value[i].Repeat--;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    await _botClient.SendTextMessageAsync(Secrets.OwnerChatId, "Ошибка Сервиса ! EXCEPTION:" + e.Message, cancellationToken: cancellationToken);
                }

                _updateServiceStatus?.Invoke(DateTime.Now.ToLongTimeString()
                                             + " Пользователей:" + totalUsers
                                             + " Задач Активных/Всего:"+ activeTasks+"/"+ totalTasks
                );


                timeOutToSaveAll--;
                if (timeOutToSaveAll == 0)
                {
                    PeriodicalSaveAll();
                    timeOutToSaveAll = TimeOutToSaveAll;
                }

                Thread.Sleep(1000);
                //_writeDebugInfo?.Invoke("Service after sleep");
            }
        }
    }
}
