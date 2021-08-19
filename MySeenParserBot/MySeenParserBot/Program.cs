using System;
using System.Reflection;
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
            AppDomain.CurrentDomain.AssemblyResolve += ((sender, eventArgs) =>
            {
                var dllName = eventArgs.Name.Contains(",") ? eventArgs.Name.Substring(0, eventArgs.Name.IndexOf(',')) : eventArgs.Name.Replace(".dll", "");
                dllName = dllName.Replace(".", "_");
                if (dllName.EndsWith("_resources"))
                    return null;

                //var rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly());
                var rm = new System.Resources.ResourceManager(typeof(Program).Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly());
                var bytes = (byte[])rm.GetObject(dllName);
                return Assembly.Load(bytes);
            });

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
                    MessageBox.Show("EXCEPTION e=" + e.Message);
                    MessageBox.Show("InnerException e=" + e.InnerException?.Message);
                    throw;
                }
            }
        }
    }
}
