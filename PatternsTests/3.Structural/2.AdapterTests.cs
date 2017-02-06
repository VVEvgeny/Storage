using NUnit.Framework;

namespace Structural
{
    [TestFixture()]
    public class AdapterTests
    {
        [Test()]
        public void ClassType1Test()
        {
            var classType1 = new ClassType1();
            Assert.AreEqual(classType1.Name, classType1.Name);
        }
        [Test()]
        public void ClassType2Test()
        {
            var classType2 = new ClassType2();
            Assert.AreEqual(classType2.MyName, classType2.MyName);

            var classType2AdapterToClassType1 = new Adapter(new ClassType2());
            Assert.AreEqual(classType2.MyName, classType2AdapterToClassType1.Name);
        }
    }
}