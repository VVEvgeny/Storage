using System;
using vvevgeny_storage;

namespace Generating
{
    public sealed class SingletonTestView : ITestView
    {
        public void Run()
        {
            Console.WriteLine($"{nameof(Singleton)}={++Singleton.Instance.Count}");
            Console.WriteLine($"{nameof(Singleton)}={++Singleton.Instance.Count}");
            Console.WriteLine($"{nameof(Singleton)}={--Singleton.Instance.Count}");
            Console.WriteLine($"{nameof(Singleton)}={--Singleton.Instance.Count}");
        }
    }
    public sealed class SingletonLazyObjectTestView : ITestView
    {
        public void Run()
        {
            Console.WriteLine($"{nameof(SingletonLazyObject)}={++SingletonLazyObject.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazyObject)}={++SingletonLazyObject.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazyObject)}={--SingletonLazyObject.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazyObject)}={--SingletonLazyObject.Instance.Count}");
        }
    }

    public sealed class SingletonLazyTestView : ITestView
    {
        public void Run()
        {
            Console.WriteLine($"{nameof(SingletonLazy)}={++SingletonLazy.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazy)}={++SingletonLazy.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazy)}={--SingletonLazy.Instance.Count}");
            Console.WriteLine($"{nameof(SingletonLazy)}={--SingletonLazy.Instance.Count}");
        }
    }
}