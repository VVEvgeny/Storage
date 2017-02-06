using System;
using vvevgeny_storage;

namespace Generating
{
    public sealed class BuilderTestView : ITestView
    {
        public void Run()
        {
            var cl = new Class();

            cl.Do1();
            cl.Do2();
            cl.Action();
            cl.Do1();

            Console.WriteLine("----------");

            new BuilderClass().Do1().Do2().Action();
        }
    }
}