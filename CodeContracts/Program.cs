using System.Diagnostics.Contracts;

namespace CodeContracts
{
    class Program
    {
        private static object o = null;
        private static int TestMethod()
        {
            //ErrorInPreConditions
            Contract.Requires(o != null);

            //ErrorInPostConditions
            Contract.Ensures(o is int);

            //ErrorInReturn
            Contract.Ensures(Contract.Result<int>() > 5);

            return (int?) o ?? 0;
        }
        
        static void Main(string[] args)
        {
            //o = 5;
            TestMethod();
        }
    }
}
