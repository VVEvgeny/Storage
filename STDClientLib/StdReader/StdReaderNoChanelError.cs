using System;

namespace STDClientLib.StdReader
{
    public class StdReaderNoChanelError : Exception
    {
        public StdReaderNoChanelError(string str) : base(str)
        {

        }
    }
}