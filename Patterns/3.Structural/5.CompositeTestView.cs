using System;
using vvevgeny_storage;

namespace Structural
{
    public sealed class CompositeTestView : ITestView
    {
        public void Run()
        {
            AClass cl1 = new CompositeClass(nameof(cl1));
            AClass composite1 = new Composite();
            composite1.Add(cl1);

            AClass cl2 = new CompositeClass(nameof(cl2));
            composite1.Add(cl2);

            Console.WriteLine(composite1.Get());

            AClass composite2 = new Composite();
            composite2.Add(cl2);
            composite2.Add(composite1);

            Console.WriteLine(composite2.Get());
        }
    }
}