//Remain:

//Generating:
//-AbstractFactory

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
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine($"-------------------------------");
                foreach (Modes m in Enum.GetValues(typeof(Modes)))
                {
                    if (m == Modes.Unknown) continue;
                    Console.WriteLine($"{(int)m}-{m}");
                }
                Console.WriteLine($"-------------------------------");

                ITestView testView = null;

                var mode = Console.ReadLine().GetMode();
                Console.Clear();

                switch (mode)
                {
                    case Modes.Exit: return;
                    case Modes.Singleton:
                        testView = new SingletonTestView();
                        break;
                    case Modes.SingletonLazyObject:
                        testView = new SingletonLazyObjectTestView();
                        break;
                    case Modes.SingletonLazy:
                        testView = new SingletonLazyTestView();
                        break;
                        case Modes.FactoryMethod:
                        testView = new FactoryMethodTestView();
                        break;
                    case Modes.Builder:
                        testView = new BuilderTestView();
                        break;
                    case Modes.Unknown:
                        break;
                    case Modes.Decorator:
                        testView = new DecoratorTestView();
                        break;
                    case Modes.Observer:
                        testView = new ObserverTestView();
                        break;
                    case Modes.Adapter:
                        testView = new AdapterTestView();
                        break;
                    case Modes.Facade:
                        testView = new FacadeTestView();
                        break;
                    case Modes.Proxy:
                        testView = new ProxyTestView();
                        break;
                    case Modes.Composite:
                        testView = new CompositeTestView();
                        break;
                    case Modes.Prototype:
                        testView = new PrototypeClassTestView();
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException();
                        Console.WriteLine("Incorrect mode");
                        break;
                }

                testView?.Run();
            } while (true);
        }
    }
}
