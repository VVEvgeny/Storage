using System;
using vvevgeny_storage;

namespace Generating
{
    public sealed class PrototypeClassTestView : ITestView
    {
        public void Run()
        {
            Console.WriteLine("Create object ConcretePrototype1");
            PrototypeClass prototype = new ConcretePrototype1("1");
            PrototypeClass clone = prototype.Clone();
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);

            Console.WriteLine("Cloned change property");
            clone.Id.Str = "5";
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);


            Console.WriteLine("Create object ConcretePrototype2");
            prototype = new ConcretePrototype2("2");
            clone = prototype.Clone();
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);

            Console.WriteLine("Cloned change property");
            clone.Id.Str = "6";
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);

            Console.WriteLine("Create object ConcretePrototype2");
            prototype = new ConcretePrototype2("3");
            clone = prototype.Clone2();
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);

            Console.WriteLine("Cloned change property by MemberwiseClone");
            clone.Id.Str = "7";
            Console.WriteLine(prototype.Name);
            Console.WriteLine(clone.Name);
        }
    }
}