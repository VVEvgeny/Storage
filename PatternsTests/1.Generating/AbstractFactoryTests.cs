using NUnit.Framework;

namespace Generating
{
    [TestFixture()]
    public class AbstractFactoryTests
    {
        [Test()]
        public void AbstractFactoryTest()
        {
            AbstractFactory abstractFactory1 = new ConcreteFactory1();
            AbstractFactory abstractFactory2 = new ConcreteFactory2();

            AbstractClassA abstractClassA1 = abstractFactory1.CreateClassA();
            AbstractClassA abstractClassA2 = abstractFactory2.CreateClassA();

            AbstractClassB abstractClassB1 = abstractFactory1.CreateClassB();
            AbstractClassB abstractClassB2 = abstractFactory2.CreateClassB();

            Assert.AreEqual(abstractClassA1.Name, nameof(ClassA1));
            Assert.AreEqual(abstractClassA2.Name, nameof(ClassA2));

            Assert.AreEqual(abstractClassB1.Name, nameof(ClassB1));
            Assert.AreEqual(abstractClassB2.Name, nameof(ClassB2));
        }
        
    }
}