using NUnit.Framework;

namespace Generating
{
    [TestFixture()]
    public class PrototypeTests
    {
        [Test()]
        public void PrototypeClone1Test()
        {
            PrototypeClass prototype = new ConcretePrototype1("1");
            PrototypeClass clone = prototype.Clone();

            Assert.AreEqual(prototype.Name, clone.Name);
        }

        [Test()]
        public void PrototypeClone2Test()
        {
            PrototypeClass prototype = new ConcretePrototype1("1");
            PrototypeClass clone = prototype.Clone2();

            Assert.AreEqual(prototype.Name, clone.Name);
        }

        [Test()]
        public void PrototypeClone1ChangeTest()
        {
            PrototypeClass prototype = new ConcretePrototype1("1");
            PrototypeClass clone = prototype.Clone();
            clone.Id.Str = "2";

            Assert.AreNotEqual(prototype.Name, clone.Name);
            Assert.AreEqual(prototype.Name,nameof(ConcretePrototype1)+" 1");
            Assert.AreEqual(clone.Name, nameof(ConcretePrototype1) + " 2");
        }

        [Test()]
        public void PrototypeClone2ChangeTest()
        {
            PrototypeClass prototype = new ConcretePrototype1("1");
            PrototypeClass clone = prototype.Clone2();
            clone.Id.Str = "2";

            Assert.AreEqual(prototype.Name, clone.Name);
            Assert.AreEqual(prototype.Name, nameof(ConcretePrototype1) + " 2");
            Assert.AreEqual(clone.Name, nameof(ConcretePrototype1) + " 2");
        }
    }
}