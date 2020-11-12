using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asyncs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private CancellationTokenSource _cts;
        private async void buttonStartTask_Click(object sender, EventArgs e)
        {
            buttonStartTask.Enabled = false;
            buttonStopTask.Enabled = true;

            try
            {
                _cts = new CancellationTokenSource();
                //_cts.CancelAfter(TimeSpan.FromSeconds(1));
                var token = _cts.Token;

                var someAsyncWork = Task.Delay(TimeSpan.FromSeconds(10), token);

                await someAsyncWork;
                MessageBox.Show("Task completed successfully");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Task canceled by user");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Task exception");
                throw;
            }
            finally
            {
                buttonStartTask.Enabled = true;
                buttonStopTask.Enabled = false;
            }
        }

        private void buttonStopTask_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            buttonStopTask.Enabled = false;
        }
    }
}
