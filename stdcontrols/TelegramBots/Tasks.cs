using System;

namespace stdcontrols.TelegramBots
{
    public class Tasks
    {
        public int Id;
        public string Name;

        private string _status;
        public string Status {
            get => _status;
            set
            {
                _status = value;
                StatusChanged?.Invoke("Task "+ Id + " new state: " + value);
            }
        }

        public string StartStatus;
        public Action<object, EventArgs> StartStopAction;

        public delegate void StatusHandler(string message);
        public event StatusHandler StatusChanged;

        public Action<string> SendCommand;

        public string Start()
        {
            if (StartStatus == "Start")
            {
                StartStopAction(null,null);
                return "Ok";
            }

            return "Already started " + Status;
        }

        public string Stop()
        {
            if (StartStatus == "Stop")
            {
                StartStopAction(null, null);
                return "Ok";
            }

            return "Already stopped " + Status;
        }

        public string Send(string message)
        {
            if (StartStatus == "Start")
            {
                return "Stopped " + Status;
            }

            SendCommand(message);

            return "Ok";
        }
    }
}
