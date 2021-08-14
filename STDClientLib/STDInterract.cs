using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using STDClientLib.StdReader;

namespace STDClientLib
{
    public class StdInteract
    {
        private readonly CancellationTokenSource _cts;
        private readonly StdReader.StdReader _std;
        private readonly TextReader _reader;
        private readonly MyCommands.MyCommands _commandProcessor;
        private readonly Action _onError;
        public StdInteract(TextReader reader, Action<string> write, MyCommands.MyCommands commandProcessor, Action onError)
        {
            _reader = reader;
            _commandProcessor = commandProcessor;
            _onError = onError;

            _cts = new CancellationTokenSource();
            _std = new StdReader.StdReader(write, _cts.Token);
        }

        public async Task StartAsync()
        {
           await Task.Run(Start);
        }
        public void Start()
        {
            try
            {
                Task.WaitAll(
                    _std.ReadStdInAsync(_reader, _commandProcessor)
                );
            }
            catch (StdReaderNoChanelError)
            {
                _cts?.Cancel();
                _onError();
            }
            catch (Exception ee)
            {
                if (ee.InnerException is StdReaderNoChanelError)
                {
                    _cts?.Cancel();
                    _onError?.Invoke();
                }
            }
        }

        public void Stop()
        {
            _cts.Cancel();
        }
    }
}
