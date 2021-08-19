using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using MySeenParserBot.TelegramBots.MySeenParserBot;
using STDClientLib;
using STDClientLib.MyCommands;

namespace MySeenParserBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var c = new MyCommands();
            c.AddCommand(new Exit());
            _std = new StdInteract(Console.In, Write, c, null);
        }
        public class Exit : IMyCommands
        {
            public string Name => "Exit";

            public string Action(string str)
            {
                Application.Exit();
                return "Ok";
            }
        }
        private void Write(string s)
        {
            lock (Console.Out)
            {
                Console.WriteLine(s);
                Console.Out.Flush();
            }
        }

        private static Bot _bot;

        private void WriteRichText(string text)
        {
            lock (richTextBox1)
            {
                richTextBox1.Text += text + Environment.NewLine;
            }
        }
        private readonly object _writeDebugToFileSync = new object();

        private void WriteDebugToFile(string text)
        {
            lock (_writeDebugToFileSync)
            {
                using (StreamWriter wr = new StreamWriter(Environment.CurrentDirectory + "\\" + "debug.txt", true))
                {
                    wr.WriteLine(text);
                    wr.Flush();
                    wr.Close();
                }
            }
        }
        private void WriteLabel(string text)
        {
            lock (label1)
            {
                label1.Text = text;
            }
        }


        private void buttonStartService_Click(object sender, EventArgs e)
        {
            if (buttonStartService.Text == "Start Service")
            {
                buttonStartService.Text = "Stop Service";
                _bot = new Bot();
                _bot.SetDebugHook(WriteRichText);
                _bot.SetServiceStatusHook(WriteLabel);
                _bot.StartAsService();
            }
            else
            {
                buttonStartService.Text = "Start Service";
                _bot.StopService();
            }
        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lock (richTextBox1)
            {
                richTextBox1.Text = "";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _bot?.StopService();
            Hide();

            _std.Stop();
            Thread.Sleep(1000);
        }

        private readonly StdInteract _std;
        private async void MainForm_Load(object sender, EventArgs e)
        {
            buttonStartService_Click(sender, e);
            DebugGlobal.WriteDebug = WriteDebugToFile;
            DebugGlobal.WriteDebug += WriteRichText;

            await _std.StartAsync();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ret = _bot.BotTasks.GetAllTasks();
            WriteRichText((string.IsNullOrEmpty(ret)
                ? "Пусто"
                : ("Пользователь / Задача / Активно / Ссылка " + Environment.NewLine + ret)));
        }
    }
}