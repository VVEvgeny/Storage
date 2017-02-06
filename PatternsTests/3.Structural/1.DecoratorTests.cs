using NUnit.Framework;

namespace Structural
{
    [TestFixture()]
    public class DecoratorTests
    {
        [Test()]
        public void BaseDecoratorTest()
        {
            IBaseClass baseClass = new BaseClass();
            Assert.AreEqual(baseClass.Count(), 1);

            baseClass = new Decorator1(new Decorator2(baseClass));
            Assert.AreEqual(baseClass.Count(), 3);

            for (;;)
            {
                if (baseClass is IUnDecorate)
                    baseClass = (baseClass as IUnDecorate).UnDecorate();
                else break;
            }
            Assert.AreEqual(baseClass.Count(), 1);
        }

        [Test()]
        public void Derived1DecoratorTest()
        {
            IBaseClass derived1 = new Derived1();
            Assert.AreEqual(derived1.Count(), 1);

            derived1 =
                new Decorator1(new Decorator2(new Decorator1(new Decorator1(new Decorator2(new Decorator2(derived1))))));
            Assert.AreEqual(derived1.Count(), 7);

            for (;;)
            {
                if (derived1 is IUnDecorate)
                    derived1 = (derived1 as IUnDecorate).UnDecorate();
                else break;
            }
            Assert.AreEqual(derived1.Count(), 1);
        }

        [Test()]
        public void DecoratorTest()
        {
            IBaseClass derived2 = new Derived2();
            Assert.AreEqual(derived2.Count(), 1);

            derived2 =
                new Decorator1(
                    new Decorator2(
                        new Decorator1(
                            new Decorator1(
                                new Decorator2(
                                    new Decorator2(
                                        new Decorator1(
                                            new Decorator2(new Decorator1(new Decorator1(new Decorator2(new Decorator2(
                                                new Decorator1(new Decorator2(
                                                    new Decorator1(new Decorator1(new Decorator2(new Decorator2(
                                                        new Decorator1(new Decorator2(
                                                            new Decorator1(
                                                                new Decorator1(
                                                                    new Decorator2(
                                                                        new Decorator2(new Decorator1(new Decorator2(
                                                                            new Decorator1(
                                                                                new Decorator1(
                                                                                    new Decorator2(new Decorator2
                                                                                        (
                                                                                        new Decorator1(
                                                                                            new Decorator2(
                                                                                                new Decorator1(
                                                                                                    new Decorator1
                                                                                                        (new Decorator2
                                                                                                            (new Decorator2
                                                                                                                (
                                                                                                                new Decorator1
                                                                                                                    (new Decorator2
                                                                                                                        (new Decorator1
                                                                                                                            (new Decorator1
                                                                                                                                (new Decorator2
                                                                                                                                    (new Decorator2
                                                                                                                                        (
                                                                                                                                        new Decorator1
                                                                                                                                            (new Decorator2
                                                                                                                                                (new Decorator1
                                                                                                                                                    (new Decorator1
                                                                                                                                                        (new Decorator2
                                                                                                                                                            (new Decorator2
                                                                                                                                                                (
                                                                                                                                                                new Decorator1
                                                                                                                                                                    (new Decorator2
                                                                                                                                                                        (new Decorator1
                                                                                                                                                                            (new Decorator1
                                                                                                                                                                                (new Decorator2
                                                                                                                                                                                    (new Decorator2
                                                                                                                                                                                        (derived2))))))))))))))))))))))))))))))))))))))))))))))))))))));
            Assert.AreEqual(derived2.Count(), 55);

            for (;;)
            {
                if (derived2 is IUnDecorate)
                    derived2 = (derived2 as IUnDecorate).UnDecorate();
                else break;
            }
            Assert.AreEqual(derived2.Count(), 1);
        }
    }
}