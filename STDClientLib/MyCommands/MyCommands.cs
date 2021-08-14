using System;
using System.Collections.Generic;
using System.Text;

namespace STDClientLib.MyCommands
{
    public class MyCommands
    {
        private readonly Dictionary<string, Func<string,string>> _list = new Dictionary<string, Func<string, string>>();
        public MyCommands()
        {
            AddCommand(new Ping());

            _list.Add("List", GetCommands);
        }

        public bool AddCommand(IMyCommands com)
        {
            if (_list.ContainsKey(com.Name))
                return false;

            _list.Add(com.Name, com.Action);
            return true;
        }

        public string GetCommands(string prefix = "")
        {
            var sb = new StringBuilder();

            foreach (var l in _list)
            {
                if (string.IsNullOrEmpty(prefix) || l.Key.StartsWith(prefix))
                {
                    sb.Append(l.Key);
                    sb.Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }


        public string Process(string command)
        {
            var cmd = command.Split(' ')[0];
            var args = command.Remove(0, cmd.Length);

            if (_list.ContainsKey(cmd))
                return _list[cmd](args);

            return "Unknown command: " + cmd;
        }
    }
}
