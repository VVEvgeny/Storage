using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using STDClientLib.StdReader;

namespace STDClientLib
{
    public class MyTask
    {
        private readonly CancellationTokenSource _cts;
        private readonly StdReader.StdReader _std;
        private readonly Action<string> _write;
        private readonly Action<string> _status;

        public MyTask(Action<string> write, Action<string> status = null)
        {
            _write = write;
            _status = status;
            _cts = new CancellationTokenSource();
            _std = new StdReader.StdReader(_write, _cts.Token);
        }

        public void Run(string path)
        {
            Task.Run(() => { Start(path); }, _cts.Token);
        }

        public void Stop()
        {
            _status?.Invoke("Stopped");
            _streamWriter?.WriteLine("Exit");
            _cts.Cancel();
        }

        public void SendCommand(string command)
        {
            if (command == "Exit")
            {
                Stop();
            }
            else
            {
                _streamWriter?.WriteLine(command);
            }
        }

        private Process _process;
        private StreamWriter _streamWriter;
        private void Start(string fileName)
        {
            _process = new Process
            {
                StartInfo =
                    {
                        FileName = fileName,
                        UseShellExecute = false,
                        WorkingDirectory = Environment.CurrentDirectory,
                        ErrorDialog = true,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true
                    }
            };

            _process.Start();

            _streamWriter = _process.StandardInput;
            _streamWriter.AutoFlush = true;

            _status?.Invoke("PID:" + _process.Id);

            try
            {
                Task.WaitAll(
                    _std.ReadStdInAsync(_process.StandardOutput, "(" + _process.Id + ")out: "),
                    _std.ReadStdInAsync(_process.StandardError, "(" + _process.Id + ")err: ")
                );
            }
            catch (StdReaderNoChanelError ee)
            {
                _write("(" + _process.Id + ")exc: " + ee.Message);
                _cts?.Cancel();
                _status?.Invoke("Stopped");
            }
            catch (Exception e)
            {
                if (e.InnerException is StdReaderNoChanelError)
                    _write("(" + _process.Id + ")exc: " + e.InnerException.Message);
                _cts?.Cancel();
                _status?.Invoke("Stopped");
            }
            finally
            {
                _streamWriter.Close();
                _streamWriter = null;
                _process.Close();
                _process = null;
            }
        }


    }
}
