namespace MySeenParserBot
{
    public static class DebugGlobal
    {
        public delegate void WriteDebugInfo(string message);

        public static WriteDebugInfo WriteDebug = null;

        public static void Write(string str)
        {
            WriteDebug?.Invoke(str);
        }
    }
}
