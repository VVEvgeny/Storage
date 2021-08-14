using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace STDClientLib.StdReader
{
    public class StdReader
    {
        private readonly Action<string> _writer;
        private readonly CancellationToken _ct;
        public StdReader(Action<string> writer, CancellationToken ct)
        {
            _writer = writer;
            _ct = ct;
        }

        public void ReadStdIn(TextReader streamReader, MyCommands.MyCommands commandProcessor = null, string prepend = "")
        {
            int nullInRow = 0;
            const int maxNullInRow = 20;
            bool isNullObtained = false;
            while (true)
            {
                if (_ct.IsCancellationRequested)
                    return;

                string line;
                if (!string.IsNullOrEmpty(line = streamReader.ReadLine()))
                {
                    if (commandProcessor != null)
                    {
                        _writer(commandProcessor.Process(line));
                    }
                    else
                    {
                        _writer("received:" + prepend + line);
                    }

                    nullInRow = 0;
                    Thread.Sleep(100);
                }
                else
                {
                    if (isNullObtained)
                    {
                        nullInRow++;
                        if (nullInRow >= maxNullInRow)
                        {
                            throw new StdReaderNoChanelError("received NULL");
                        }
                    }
                    isNullObtained = true;
                    Thread.Sleep(100);
                }

            }
        }
        public void ReadStdIn(TextReader streamReader, string prepend = "")
        {
            ReadStdIn(streamReader,null, prepend);
        }
        public async Task ReadStdInAsync(TextReader streamReader, MyCommands.MyCommands commandProcessor = null)
        {
            await Task.Run(() => { ReadStdIn(streamReader, commandProcessor); }, _ct);
        }
        public async Task ReadStdInAsync(TextReader streamReader, string prepend)
        {
            await Task.Run(() => { ReadStdIn(streamReader, null, prepend); }, _ct);
        }
    }
}
