using System;
using System.Threading;
using System.Windows.Forms;
using MySeenParserBot.TelegramBots.MySeenParserBot;

namespace MySeenParserBot
{
    public partial class MainForm : Form
    {
        private static Bot _bot = new Bot();

        public MainForm()
        {
            InitializeComponent();
        }

        private void WriteRichText(string text)
        {
            lock (richTextBox1)
            {
                richTextBox1.Text += text + Environment.NewLine;
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
            Thread.Sleep(1000);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            buttonStartService_Click(sender, e);
        }
    }
}