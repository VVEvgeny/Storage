using System;
using System.Collections.Generic;
using vvevgeny_storage;

namespace Behavioural
{
    public sealed class ObserverTestView : ITestView
    {
        public void Run()
        {
            var observerSample = new ObserverSample();

            var observers = new List<Observer>();
            for (var i = 0; i < 10; i++)
            {
                observers.Add(new Observer(i));
            }
            foreach (var observer in observers)
            {
                observerSample.Update += observer.GetUpdate;
            }
            observerSample.GenerateUpdate("1");

            Console.WriteLine("--------------");
            for (var i = 0; i < 10; i++)
            {
                if (i % 2 == 0) observerSample.Update -= observers[i].GetUpdate;
            }
            observerSample.GenerateUpdate("2");

            Console.WriteLine("--------------");
            for (var i = 0; i < 10; i++)
            {
                if (i % 4 == 0) observerSample.Update += observers[i].GetUpdate;
            }
            observerSample.GenerateUpdate("3");


            Console.WriteLine("--------------");
            for (var i = 0; i < 10; i++)
            {
                if (i > 5) observerSample.Update -= observers[i].GetUpdate;
            }
            observerSample.GenerateUpdate("4");

            Console.WriteLine("--------------");
            for (var i = 0; i < 10; i++)
            {
                observerSample.Update -= observers[i].GetUpdate;
            }
            observerSample.GenerateUpdate("5");
        }
    }
}