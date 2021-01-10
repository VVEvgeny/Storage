using System;
using System.Collections.Generic;

namespace MySeenParserBot.TelegramBots.MySeenParserBot
{
    public static class BotTaskExt
    {
        public static void ResetInterval(this BotTasks.BotTask task)
        {
            task.Repeat = 60;
        }
    }
    public class BotTasks
    {
        private static int _id = 1;

        public object IdSync = new object();
        public int GetId()
        {
            lock (IdSync)
            {
                return _id++;
            }
        }

        public int SetId(int id)
        {
            lock (IdSync)
            {
                return _id = id;
            }
        }

        [Serializable]
        public class BotTask
        {
            public int TaskId;
            public string Request;
            public string Data;
            public int Repeat;//seconds
            public bool IsActive;
        }

        public Dictionary<long, List<BotTask>> ActiveTasks = new Dictionary<long, List<BotTask>>();//user/tasks

        public BotTask AddTask(long userId, string link)
        {
            lock (ActiveTasks)
            {
                if (!ActiveTasks.ContainsKey(userId))
                    ActiveTasks.Add(userId, new List<BotTask>());

                ActiveTasks[userId].Add(new BotTask { TaskId = GetId(), IsActive = true, Request = link });
                return ActiveTasks[userId][ActiveTasks[userId].Count - 1];
            }
        }

        public string GetAllTasks()
        {
            var tasks = "";
            lock (ActiveTasks)
            {
                foreach (var tt in ActiveTasks)
                {
                    foreach (var t in tt.Value)
                    {
                        tasks += (tt.Key + " " + t.TaskId + (t.IsActive ? " + " : " - ") + t.Request) + " Обновление через:" + t.Repeat +
                                 Environment.NewLine;
                    }
                }
            }

            return tasks;
        }

        public string GetMyTasks(long userId)
        {
            var tasks = "";

            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    foreach (var t in ActiveTasks[userId])
                    {
                        tasks += (t.TaskId + (t.IsActive ? " + " : " - ") + t.Request) + " Обновление через:" +
                                 t.Repeat +
                                 Environment.NewLine;
                    }
                }
            }

            return tasks;
        }

        public bool PauseMyTask(long userId, int taskId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (var i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        var t = ActiveTasks[userId][i];
                        if (t.TaskId == taskId)
                        {
                            if (!t.IsActive)
                                return false;

                            t.IsActive = false;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool UnPauseMyTask(long userId, int taskId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (var i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        var t = ActiveTasks[userId][i];
                        if (t.TaskId == taskId)
                        {
                            if (t.IsActive)
                                return false;

                            t.IsActive = true;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool RemoveMyTask(long userId, int taskId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (var i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        if (ActiveTasks[userId][i].TaskId == taskId)
                        {
                            ActiveTasks[userId].Remove(ActiveTasks[userId][i]);

                            if(ActiveTasks[userId].Count == 0)
                                ActiveTasks.Remove(userId);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public void PauseAllMyTasks(long userId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (var i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        ActiveTasks[userId][i].IsActive = false;
                    }
                }
            }
        }
        public void UnPauseAllMyTasks(long userId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (var i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        ActiveTasks[userId][i].IsActive = true;
                    }
                }
            }
        }
        public void RemoveAllMyTasks(long userId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    ActiveTasks[userId].Clear();
                }
            }
        }
        public void PauseAllTasks()
        {
            lock (ActiveTasks)
            {
                foreach (var at in ActiveTasks)
                {
                    foreach (var task in at.Value)
                    {
                        task.IsActive = false;
                    }
                }
            }
        }
        public void UnPauseAllTasks()
        {
            lock (ActiveTasks)
            {
                foreach (var at in ActiveTasks)
                {
                    foreach (var task in at.Value)
                    {
                        task.IsActive = true;
                    }
                }
            }
        }

        public void RemoveAllTasks()
        {
            lock (ActiveTasks)
            {
                ActiveTasks.Clear();
            }
        }

        public delegate void SaveDataProcessTaskDelegate(long userId, int taskId, string newData);

        public void SaveDataProcessTask(long userId, int taskId, string newData)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (int i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        if (ActiveTasks[userId][i].TaskId == taskId)
                        {
                            ActiveTasks[userId][i].Data = newData;
                            break;
                        }
                    }
                }
            }
        }

        public delegate void OnDeleteWithParsingDelegate(long userId, int taskId);

        public void OnDeleteWithParsing(long userId, int taskId)
        {
            lock (ActiveTasks)
            {
                if (ActiveTasks.ContainsKey(userId))
                {
                    for (int i = 0; i < ActiveTasks[userId].Count; i++)
                    {
                        if (ActiveTasks[userId][i].TaskId == taskId)
                        {
                            ActiveTasks[userId].Remove(ActiveTasks[userId][i]);

                            if (ActiveTasks[userId].Count == 0)
                                ActiveTasks.Remove(userId);

                            return;
                        }
                    }
                }
            }
        }
    }
}