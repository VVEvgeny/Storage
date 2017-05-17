using System.Diagnostics.Contracts;
using NUnit.Framework;

namespace CodeContracts.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void GoodTest()
        {
            var handled = false;
            Contract.ContractFailed += (sender, e) =>
            {
                e.SetHandled();
                handled = true;
            };

            Program.TestMethod(6);

            Assert.IsFalse(handled);
        }

        [Test]
        public void PostConditionsTest()
        {
            var handled = false;
            Contract.ContractFailed += (sender, e) =>
            {
                e.SetHandled();
                handled = true;
            };

            Program.TestMethod("a");

            Assert.IsTrue(handled);
        }

        [Test]
        public void PreConditionsTest()
        {
            var handled = false;
            Contract.ContractFailed += (sender, e) =>
            {
                e.SetHandled();
                handled = true;
            };

            Program.TestMethod();

            Assert.IsTrue(handled);
        }

        [Test]
        public void ReturnTest()
        {
            var handled = false;
            Contract.ContractFailed += (sender, e) =>
            {
                e.SetHandled();
                handled = true;
            };

            Program.TestMethod(3);

            Assert.IsTrue(handled);
        }
    }
}