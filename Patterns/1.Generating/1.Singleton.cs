using System;

namespace Generating
{
    public sealed class Singleton
    {
        public int Count;

        private static Singleton _instance;
        public static Singleton Instance => _instance ?? (_instance = new Singleton());
    }

    public sealed class SingletonLazyObject
    {
        public int Count;

        private static readonly object Sync = new object();
        private static volatile SingletonLazyObject _instance;

        public static SingletonLazyObject Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonLazyObject();
                        }
                    }
                }
                return _instance;
            }
        }
    }

    public sealed class SingletonLazy
    {
        public int Count;

        private static readonly Lazy<SingletonLazy> _instance = new Lazy<SingletonLazy>(() => new SingletonLazy());

        public static SingletonLazy Instance => _instance.Value;
    }
}