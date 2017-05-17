//Remain:

//Generating:
//-

//Behavioural:
//-Strategy
//-Command
//-TemplateMethod
//-Iterator
//-State
//-ChainOfResponsibility
//-Interpreter
//-Mediator
//-Memento
//-Visitor

//Structural:
//-Bridge
//-Flyweight



using System;
using Behavioural;
using Generating;
using Structural;

namespace vvevgeny_storage
{
    public interface ITestView
    {
        void Run();
    }

    public enum Modes
    {
        Exit,
        Singleton,
        SingletonLazyObject,
        SingletonLazy,
        FactoryMethod,
        Builder,
        Decorator,
        Observer,
        Adapter,
        Facade,
        Proxy,
        Composite,
        Prototype,
        AbstractFactory,
        Unknown = int.MaxValue
    }

    public static class ModesExtension
    {
        public static Modes GetMode(this string value)
        {
            Modes i;
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            return Enum.TryParse<Modes>(value, out i) ? i : Modes.Unknown;
        }
    }


    public class Program
    {
        private static ITestView Create(Modes mode)
        {
            switch (mode)
            {
                case Modes.Exit:
                    Environment.Exit(0);
                    break;
                case Modes.Singleton:
                    return new SingletonTestView();
                case Modes.SingletonLazyObject:
                    return new SingletonLazyObjectTestView();
                case Modes.SingletonLazy:
                    return new SingletonLazyTestView();
                case Modes.FactoryMethod:
                    return new FactoryMethodTestView();
                case Modes.Builder:
                    return new BuilderTestView();
                case Modes.Decorator:
                    return new DecoratorTestView();
                case Modes.Observer:
                    return new ObserverTestView();
                case Modes.Adapter:
                    return new AdapterTestView();
                case Modes.Facade:
                    return new FacadeTestView();
                case Modes.Proxy:
                    return new ProxyTestView();
                case Modes.Composite:
                    return new CompositeTestView();
                case Modes.Prototype:
                    return new PrototypeClassTestView();
                case Modes.AbstractFactory:
                    return new AbstractFactoryTestView();
                default:
                    //throw new ArgumentOutOfRangeException();
                    Console.WriteLine("Incorrect mode");
                    break;
            }
            return null;
        }

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine($"-------------------------------");
                foreach (Modes m in Enum.GetValues(typeof(Modes)))
                {
                    if (m == Modes.Unknown) continue;
                    Console.WriteLine($"{(int) m}-{m}");
                }
                Console.WriteLine($"-------------------------------");

                var mode = Console.ReadLine().GetMode();
                Console.Clear();

                var testView = Create(mode);

                testView?.Run();
            } while (true);
        }
    }
}
