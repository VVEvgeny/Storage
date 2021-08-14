namespace STDClientLib.MyCommands
{
    public interface IMyCommands
    {
        string Name { get; }
        string Action(string str);
    }
}