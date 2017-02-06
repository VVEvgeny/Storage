using NUnit.Framework;

namespace Generating
{
    [TestFixture()]
    public class FactoryMethodTests
    {
        [Test()]
        public void ClassDerived1Test()
        {
            Assert.AreEqual(ClassFactory.Create<ClassDerived1>().Action, nameof(ClassDerived1));
        }

        [Test()]
        public void ClassDerived2Test()
        {
            Assert.AreEqual(ClassFactory.Create<ClassDerived2>().Action, nameof(ClassDerived2));
        }
    }
}