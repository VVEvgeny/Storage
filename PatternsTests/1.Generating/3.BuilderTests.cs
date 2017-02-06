using System;
using NUnit.Framework;

namespace Generating
{
    [TestFixture()]
    public class BuilderTests
    {
        [Test()]
        public void Correct()
        {
            try
            {
                new BuilderClass().Do1().Do2().Action();
            }
            catch (Exception)
            {
                Assert.Fail("correct work");
            }
        }

        [Test()]
        public void Exception()
        {
            var haveException = false;
            try
            {
                new BuilderClass().Do2().Do1().Action();
            }
            catch (InvalidOperationException)
            {
                haveException = true;
            }
            Assert.IsTrue(haveException, "No have exception");
        }
    }
}