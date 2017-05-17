using System;
using vvevgeny_storage;

namespace Generating
{
    public sealed class AbstractFactoryTestView : ITestView
    {
        public void Run()
        {
            AbstractFactory abstractFactory1 =new ConcreteFactory1();
            AbstractFactory abstractFactory2 = new ConcreteFactory2();

            AbstractClassA abstractClassA1 = abstractFactory1.CreateClassA();
            AbstractClassA abstractClassA2 = abstractFactory2.CreateClassA();

            AbstractClassB abstractClassB1 = abstractFactory1.CreateClassB();
            AbstractClassB abstractClassB2 = abstractFactory2.CreateClassB();

            Console.WriteLine("abstractClassA1="+ abstractClassA1.Name);
            Console.WriteLine("abstractClassA2=" + abstractClassA2.Name);
            Console.WriteLine("abstractClassB1=" + abstractClassB1.Name);
            Console.WriteLine("abstractClassB2=" + abstractClassB2.Name);
        }
    }
}