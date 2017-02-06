using NUnit.Framework;

namespace Structural
{
    [TestFixture()]
    public class FacadeTests
    {
        [Test()]
        public void Test()
        {
            var classAction1 = new ClassAction1();
            var classAction2 = new ClassAction2();
            var classAction3 = new ClassAction3();

            var result = classAction1.DoAction + classAction2.DoAction + classAction3.DoAction;

            var facade = new Facade();

            Assert.AreEqual(result, facade.DoAction);
        }
    }
}