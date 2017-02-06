using NUnit.Framework;

namespace Generating
{
    [TestFixture()]
    public class SingletonTests
    {
        private static int _operationTest;
        [SetUp]
        public void SetUp()
        {
            _operationTest = 100;
        }

        [Test()]
        public void SingletonTest()
        {
            for (var i = 0; i < _operationTest; i++)
            {
                Assert.AreEqual(Singleton.Instance.Count = i, i);
            }
        }

        [Test()]
        public void SingletonLazyObjectTest()
        {
            for (var i = 0; i < _operationTest; i++)
            {
                Assert.AreEqual(SingletonLazyObject.Instance.Count = i, i);
            }
        }

        [Test()]
        public void SingletonLazyTest()
        {
            for (var i = 0; i < _operationTest; i++)
            {
                Assert.AreEqual(SingletonLazy.Instance.Count = i, i);
            }
        }
    }
}