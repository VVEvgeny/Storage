using System;
using System.Windows.Forms;

namespace MySeenParserBot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new AssemblyResolver();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (args.Contains("-service"))
            {

            }
            //else
            {
                try
                {
                    Application.Run(new MainForm());
                }
                catch (Exception e)
                {
                    MessageBox.Show(@"EXCEPTION e=" + e.Message);
                    MessageBox.Show(@"InnerException e=" + e.InnerException?.Message);
                    throw;
                }
            }
        }
    }
}
