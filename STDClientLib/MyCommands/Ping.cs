namespace STDClientLib.MyCommands
{
    public class Ping : IMyCommands
    {
        public string Name => "Ping";

        public string Action(string str)
        {
            return "Pong";
        }
    }
}