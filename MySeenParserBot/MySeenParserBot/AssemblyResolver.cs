using System;
using System.Reflection;

namespace MySeenParserBot
{
    class AssemblyResolver
    {
        public AssemblyResolver()
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
        }
    }
}
