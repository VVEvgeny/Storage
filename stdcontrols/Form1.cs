using System;
using System.Threading;
using System.Windows.Forms;
using MySeenParserBot.TelegramBots.MySeenParserBot;
using STDClientLib;
using stdcontrols.TelegramBots;
using stdcontrols.TelegramBots.MySeenParserBot;

namespace stdcontrols
{
    public partial class Form1 : Form
    {
        private readonly MyTask[] _tasks = new MyTask[2];
        private readonly Tasks[] tasks = new Tasks[2];
        public Form1()
        {
            InitializeComponent();

            tasks[0] = new Tasks
            {
                Id = 1, Name = textBoxTask1Path.Text, Status = labelStatus1.Text, StartStatus = button6.Text,
                StartStopAction = button6_Click
            };
            tasks[1] = new Tasks
            {
                Id = 2, Name = textBoxTask2Path.Text, Status = labelStatus2.Text,
                StartStatus = button7.Text, StartStopAction = button7_Click
            };
        }



        public delegate void OutputChangedHandler(string message);
        public event OutputChangedHandler OutputChanged;
        private void Write(string s)
        {
            lock (richTextBox1)
            {
                richTextBox1.Text += s + Environment.NewLine;
                OutputChanged?.Invoke(s + Environment.NewLine);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(button6.Text == @"Stop")
                button6_Click(sender,e);

            if (button7.Text == @"Stop")
                button7_Click(sender, e);

            _bot?.StopService();
            Hide();
            Thread.Sleep(1000);
        }
        

        public void WriteStatus1(string s)
        {
            lock (labelStatus1)
            {
                labelStatus1.Text = s;
            }
        }
        public void WriteStatus2(string s)
        {
            lock (labelStatus2)
            {
                labelStatus2.Text = s;
            }
        }




        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == @"Start")
            {
                var t = new MyTask(Write, WriteStatus1);
                t.Run(textBoxTask1Path.Text);
                _tasks[0] = t;
                lock (tasks)
                {
                    tasks[0].SendCommand = _tasks[0].SendCommand;
                }

                button6.Text = @"Stop";
            }
            else
            {
                _tasks[0].Stop();
                button6.Text = @"Start";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == @"Start")
            {
                var t = new MyTask(Write, WriteStatus2);
                t.Run(textBoxTask2Path.Text);
                _tasks[1] = t;
                lock (tasks)
                {
                    tasks[1].SendCommand = _tasks[1].SendCommand;
                }
                button7.Text = @"Stop";
            }
            else
            {
                _tasks[1].Stop();
                button7.Text = @"Start";
            }
        }


        
        private void buttonSendCommand_Click(object sender, EventArgs e)
        {
            CheckBox[] chbc = { checkBoxC1, checkBoxC2 };
            for (var i = 0; i < chbc.Length; i++)
            {
                if (chbc[i].Checked)
                {
                    if (_tasks[i] != null)
                    {
                        _tasks[i].SendCommand(textBoxSendCommand.Text);
                    }
                }
            }
        }

        private void labelStatus1_TextChanged(object sender, EventArgs e)
        {
            lock (labelStatus1)
            {
                if (labelStatus1.Text == @"Stopped")
                {
                    button6.Text = @"Start";
                }
            }

            lock (tasks)
            {
                tasks[0].Status = labelStatus1.Text;
            }
        }

        private void labelStatus2_TextChanged(object sender, EventArgs e)
        {
            lock (labelStatus2)
            {
                if (labelStatus2.Text == @"Stopped")
                {
                    button7.Text = @"Start";
                }
            }
            lock (tasks)
            {
                tasks[1].Status = labelStatus2.Text;
            }
        }






        private static Bot _bot;
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == @"Start")
            {
                button1.Text = @"Stop";
                _bot = new Bot();
                lock (_tasks)
                {
                    _bot.SetTasks(tasks);
                }
                //_bot.SetDebugHook(Write);//а то будет писать сам себе...
                OutputChanged += _bot.OutputChanged;

                //_bot.SetServiceStatusHook(WriteLabel);
                _bot.StartAsService();
            }
            else
            {
                button1.Text = @"Start";
                _bot.StopService();
                OutputChanged -= _bot.OutputChanged;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lock (richTextBox1)
            {
                richTextBox1.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button6_TextChanged(object sender, EventArgs e)
        {
            lock (_tasks)
            {
                tasks[0].StartStatus = button6.Text;
            }
        }

        private void button7_TextChanged(object sender, EventArgs e)
        {
            lock (_tasks)
            {
                tasks[1].StartStatus = button7.Text;
            }
        }
    }
}
