using System;
using vvevgeny_storage;

namespace Structural
{
    public sealed class FacadeTestView : ITestView
    {
        public void Run()
        {
            var classAction1 = new ClassAction1();
            var classAction2 = new ClassAction2();
            var classAction3 = new ClassAction3();

            var result = classAction1.DoAction + classAction2.DoAction + classAction3.DoAction;
            Console.WriteLine(result);

            var facade = new Facade();
            Console.WriteLine(facade.DoAction);
        }
    }
}