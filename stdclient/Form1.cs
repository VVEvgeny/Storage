using System;
using System.Windows.Forms;
using STDClientLib;
using STDClientLib.MyCommands;

namespace stdclient
{
    public partial class Form1 : Form
    {
        private readonly StdInteract _std;
        public Form1()
        {
            InitializeComponent();
            Text = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

            var c = new MyCommands();
            c.AddCommand(new Exit());
            _std  = new StdInteract(Console.In,Write,c, null);
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

        private void WriteErr(string s)
        {
            lock (Console.Error)
            {
                Console.Error.WriteLine(s);
                Console.Error.Flush();
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
             await _std.StartAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _std.Stop();
        }
    }
}
