using NUnit.Framework;

namespace Structural
{
    [TestFixture()]
    public class ProxyTests
    {
        [Test()]
        public void TestClass()
        {
            IClass cl = new Class();
            Assert.AreEqual(cl.Action(), nameof(Class));
        }

        [Test()]
        public void TestProxy()
        {
            IClass clp = new Proxy();
            Assert.AreEqual(clp.Action(), nameof(Proxy) + nameof(Class));
        }
    }
}