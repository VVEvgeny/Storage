using System;
using vvevgeny_storage;

namespace Structural
{
    public sealed class ProxyTestView : ITestView
    {
        public void Run()
        {
            IClass cl = new Class();
            Console.WriteLine(cl.Action());

            IClass clp = new Proxy();
            Console.WriteLine(clp.Action());
        }
    }
}