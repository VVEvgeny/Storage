using System.Collections.Generic;
using NUnit.Framework;

namespace Behavioural
{
    [TestFixture()]
    public class ObserverTests
    {
        private class CheckAssert
        {
            public string NeedMessage = string.Empty;
            public bool Called = false;
            public void Check(string message)
            {
                Assert.AreEqual(message, NeedMessage);
                Called = true;
            }
        }

        [Test()]
        public void Test()
        {
            var observerSample = new ObserverSample();

            var observers = new List<CheckAssert>();
            for (var i = 0; i < 10; i++)
            {
                observers.Add(new CheckAssert());
            }

            foreach (var observer in observers)
            {
                observerSample.Update += observer.Check;
            }

            string needMessage = "1";
            foreach (var observer in observers)
            {
                observer.NeedMessage = needMessage;
            }
            observerSample.GenerateUpdate(needMessage);
            foreach (var observer in observers)
            {
                Assert.AreEqual(observer.Called, true);
            }

            needMessage = "2";
            for (var i = 0; i < observers.Count; i++)
            {
                if (i % 2 == 0) observerSample.Update -= observers[i].Check;
                observers[i].NeedMessage = needMessage;
                observers[i].Called = false;
            }
            observerSample.GenerateUpdate(needMessage);
            for (var i = 0; i < observers.Count; i++)
            {
                Assert.AreEqual(observers[i].Called, i % 2 != 0);
            }
        }
    }
}