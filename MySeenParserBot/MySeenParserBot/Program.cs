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
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ((sender, args) =>
            {
                var dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
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
