using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Timer = System.Windows.Forms.Timer;
using static MM.ThreadHelperClass;
using System.Security.Cryptography;
using System.Text;

namespace MM
{

    public partial class Form1 : Form
    {
        public class MouseOperations
        {
            [Flags]
            public enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                Move = 0x00000001,
                Absolute = 0x00008000,
                RightDown = 0x00000008,
                RightUp = 0x00000010
            }

            [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool SetCursorPos(int x, int y);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(out MousePoint lpMousePoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            public static void SetCursorPosition(int x, int y)
            {
                SetCursorPos(x, y);
            }

            public static void SetCursorPosition(MousePoint point)
            {
                SetCursorPos(point.X, point.Y);
            }

            public static MousePoint GetCursorPosition()
            {
                MousePoint currentMousePoint;
                var gotPoint = GetCursorPos(out currentMousePoint);
                if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
                return currentMousePoint;
            }

            public static void MouseEvent(MouseEventFlags value)
            {
                MousePoint position = GetCursorPosition();

                mouse_event
                    ((int)value,
                        position.X,
                        position.Y,
                        0,
                        0)
                    ;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MousePoint
            {
                public int X;
                public int Y;

                public MousePoint(int x, int y)
                {
                    X = x;
                    Y = y;
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            t = new Timer();
            t.Interval = 1000;
            t.Tick += timer_func;

            ps = new PingStatistic(this, richTextBox1, labelOverall, labelLoses, labelOk);
        }

        private Timer t;
        private int step = 1;

        private void Click()
        {
            if (Cursor.Position.X == 0)
            {
                step = 1;
            }
            else if (Screen.PrimaryScreen.Bounds.X == Cursor.Position.X)
            {
                step = -1;
            }


            //MouseOperations.SetCursorPosition(Cursor.Position.X + step, Cursor.Position.Y);


            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            Thread.Sleep(100);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

        }

        private int secs = 5;
        private void timer_func(object sender, EventArgs e)
        {
            secs--;
            if (secs <= 0)
            {
                Click();
                secs = 5;
            }

            label1.Text = secs.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                button1.Text = "Stop";
                secs = 10;
                t.Start();
            }
            else
            {
                button1.Text = "Start";
                t.Stop();
            }
        }

        private class PingStatistic
        {
            private Form f;
            private RichTextBox rtb1;
            private Timer t;
            private Label overallLabel;
            private Label losesLabel;
            private Label okLabel;

            public PingStatistic(Form f, RichTextBox rtb1, Label overallLabel, Label losesLabel, Label okLabel)
            {
                t = new Timer();
                t.Interval = 1000;
                t.Tick += CalculateStatistic;
                this.f = f;
                this.rtb1 = rtb1;
                this.overallLabel = overallLabel;
                this.losesLabel = losesLabel;
                this.okLabel = okLabel;
            }

            private void CalculateStatistic(object sender, EventArgs e)
            {
                SetText(f, rtb1, "CalculateStatistic replies:" + replies.Count+ " loses:"+ loses.Count);


                var total = replies.Count + loses.Count;
                SetText(f, overallLabel, total.ToString(), TextAppendEnum.Write);
                SetText(f, okLabel, replies.Count + " (" + ((double)replies.Count / total).ToString("0.00%") + ")", TextAppendEnum.Write);
                SetText(f, losesLabel, loses.Count + " ("+ ((double)loses.Count / total).ToString("0.00%") + ")", TextAppendEnum.Write);
            }

            private List<int> replies;
            private List<DateTime> loses;
            public void Start()
            {
                replies = new List<int>();
                loses = new List<DateTime>();
                t.Start();
            }

            public void Stop()
            {
                t.Stop();
            }

            
            public void AddReplyMs(string ms)
            {
                replies.Add(int.Parse(ms));
                SetText(f, rtb1, "AddReplyMs:" + ms);
            }

            public void AddLoss()
            {
                loses.Add(DateTime.Now);
                SetText(f, rtb1, "AddLoss");
            }
        }

        private void PingProcessor(string str)
        {
            if (str.StartsWith("Reply from "))
            {
                //Reply from 142.250.200.110: bytes=32 time=19ms TTL=116
                var ms = str.Split(' ')[4].Split('=')[1].RemoveFromEnd(2);
                SetText(this, labelLatestData,ms, TextAppendEnum.Write);

                ps.AddReplyMs(ms);
            }
            else if (str.StartsWith("Request timed out"))
            {
                SetText(this, labelLatestData, "timed out", TextAppendEnum.Write);
                ps.AddLoss();
            }
        }

        private string[] stringsToProcess = new string[] { "Reply from ", "Request timed out" };

        private Task ping;
        private CancellationTokenSource pingCancel;
        private PingStatistic ps ;
        private void button_ping_Click(object sender, EventArgs e)
        {
            if (button_ping.Text == "Start")
            {
                button_ping.Text = "Stop";
                pingCancel = new CancellationTokenSource();
                ps.Start();
                ping = Task.Run(() =>
                {
                    using var process = new Process
                    {
                        StartInfo =
                        {
                            FileName = "cmd.exe",
                            UseShellExecute = false,
                            WorkingDirectory = Environment.CurrentDirectory,
                            ErrorDialog = true,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardInput = true,
                            RedirectStandardError = true
                        }
                    };
                    process.Start();
                    var streamWriter = process.StandardInput;
                    var streamReader = process.StandardOutput;
                    //var streamReaderError = process.StandardError;
                    streamWriter.AutoFlush = true;
                    streamWriter.WriteLine("ping google.com -t");

                    while (!streamReader.EndOfStream)
                    {
                        if (pingCancel.IsCancellationRequested)
                        {
                            process.CloseMainWindow();
                            process.Close();
                            break;
                        }
                        var str = streamReader.ReadLineAsync();
                        str.ContinueWith((str) =>
                        {
                            if (!string.IsNullOrEmpty(str.Result))
                            {
                                SetText(this, richTextBoxDebug, str.Result);

                                foreach (var s in stringsToProcess)
                                {
                                    if (str.Result.StartsWith(s))
                                    {
                                        PingProcessor(str.Result);
                                    }
                                }
                            }
                        });
                        Thread.Sleep(10);
                    }
                }, pingCancel.Token);
            }
            else
            {
                button_ping.Text = "Start";
                ps.Stop();
                if (ping != null && ping.Status == TaskStatus.Running)
                {
                    pingCancel.Cancel();
                }
            }
        }

        private void richTextBoxDebug_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBoxDebug_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetText(this, richTextBoxDebug, string.Empty, TextAppendEnum.Write);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SetText(this, richTextBox1, string.Empty, TextAppendEnum.Write);
        }

        public static bool ReverseLogs = true;
        private void checkBoxReverseLogs_CheckedChanged(object sender, EventArgs e)
        {
            ReverseLogs = checkBoxReverseLogs.Checked;
        }

        public static string xorIt(string key, string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
                sb.Append((char)(input[i] ^ key[(i % key.Length)]));
            String result = sb.ToString();

            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string mm = Environment.MachineName + "_" + Environment.UserDomainName + "_" + Environment.UserName;

           //string textToEncrypt = "";
           var strl = 12; // textToEncrypt.Length;
           string textToDecrupt = "\u001d=}XsObn< \u0012\u001c\u001e;x%\u000e:\br68X5,";
           Clipboard.SetText(xorIt(textToDecrupt, mm).Substring(0, strl));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            foreach (var l in richTextBox2.Lines)
            {
                if(Directory.Exists(l))
                    Directory.Delete(l);
                else if (File.Exists(l))
                    File.Delete(l);
            }
        }
    }

    public static class ThreadHelperClass
    {
        public enum TextAppendEnum
        {
            Write,
            Append,
            AppendNewLine
        }

        delegate void SetTextCallback(Form f, Control ctrl, string text, TextAppendEnum e);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(Form form, Control ctrl, string text, TextAppendEnum e = TextAppendEnum.AppendNewLine)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = SetText;
                form.Invoke(d, form, ctrl, text, e);
            }
            else
            {
                if (e == TextAppendEnum.Append)
                {
                    if(Form1.ReverseLogs)
                        ctrl.Text = text + ctrl.Text;
                    else
                        ctrl.Text += text;
                }
                else if (e == TextAppendEnum.AppendNewLine)
                {
                    if (Form1.ReverseLogs)
                        ctrl.Text = text + Environment.NewLine + ctrl.Text;
                    else
                        ctrl.Text += text + Environment.NewLine;
                }
                else
                {
                    ctrl.Text = text;
                }
            }
        }
    }

    public static class Extensions
    {
        public static string RemoveFromEnd(this string s, int len)
        {
            return s.Remove(s.Length - len);
        }
    }
}
