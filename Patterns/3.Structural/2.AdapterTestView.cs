using System;
using vvevgeny_storage;

namespace Structural
{
    public sealed class AdapterTestView : ITestView
    {
        public void Run()
        {
            var classType1 = new ClassType1();
            Console.WriteLine(classType1.Name);

            var classType2 = new ClassType2();
            Console.WriteLine(classType2.MyName);

            var classType2AdapterToClassType1 = new Adapter(new ClassType2());
            Console.WriteLine(classType2AdapterToClassType1.Name);
        }
    }
}