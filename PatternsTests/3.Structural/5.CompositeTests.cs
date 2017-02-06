using NUnit.Framework;

namespace Structural
{
    [TestFixture()]
    public class CompositeTests
    {
        [Test()]
        public void Test()
        {
            AClass cl1 = new CompositeClass(nameof(cl1));
            AClass composite1 = new Composite();
            composite1.Add(cl1);

            AClass cl2 = new CompositeClass(nameof(cl2));
            composite1.Add(cl2);

            var get1 = composite1.Get();
            Assert.AreEqual(get1, nameof(cl1) + " " + nameof(cl2) + " ");


            AClass composite2 = new Composite();
            composite2.Add(cl2);
            composite2.Add(composite1);

            Assert.AreEqual(composite2.Get(), nameof(cl2) + " " + get1 + " ");
        }
    }
}