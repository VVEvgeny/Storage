using System;
using vvevgeny_storage;

namespace Structural
{
    public sealed class DecoratorTestView : ITestView
    {
        public void Run()
        {
            IBaseClass baseClass = new BaseClass();
            Console.WriteLine($"Class=|{baseClass.Description()}| Count={baseClass.Count()}");

            //decorators
            baseClass = new Decorator1(new Decorator2(baseClass));
            Console.WriteLine($"Class=|{baseClass.Description()}| Count={baseClass.Count()}");

            //undecorate
            for (;;)
            {
                if (baseClass is IUnDecorate)
                    baseClass = (baseClass as IUnDecorate).UnDecorate();
                else break;
            }
            Console.WriteLine($"Class=|{baseClass.Description()}| Count={baseClass.Count()}");

            Console.WriteLine("-----------------------");


            IBaseClass derived1 = new Derived1();
            Console.WriteLine($"Class=|{derived1.Description()}| Count={derived1.Count()}");

            derived1 = new Decorator2(new Decorator1(new Decorator1(new Decorator1(new Decorator2(derived1)))));
            Console.WriteLine($"Class=|{derived1.Description()}| Count={derived1.Count()}");

            for (;;)
            {
                if (derived1 is IUnDecorate)
                    derived1 = (derived1 as IUnDecorate).UnDecorate();
                else break;
            }
            Console.WriteLine($"Class=|{derived1.Description()}| Count={derived1.Count()}");

            Console.WriteLine("-----------------------");


            IBaseClass derived2 = new Derived2();
            Console.WriteLine($"Class=|{derived2.Description()}| Count={derived2.Count()}");

            derived2 = new Decorator1(new Decorator2(new Decorator1(new Decorator2(new Decorator1(new Decorator2(derived2))))));
            Console.WriteLine($"Class=|{derived2.Description()}| Count={derived2.Count()}");

            for (;;)
            {
                if (derived2 is IUnDecorate)
                    derived2 = (derived2 as IUnDecorate).UnDecorate();
                else break;
            }
            Console.WriteLine($"Class=|{derived2.Description()}| Count={derived2.Count()}");
        }
    }
}