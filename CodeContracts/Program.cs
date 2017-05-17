using System;
using System.Diagnostics.Contracts;

namespace CodeContracts
{
    public class Program
    {
        public static int TestMethod(object o = null)
        {
            //ErrorInPreConditions
            Contract.Requires(o != null);

            //ErrorInPostConditions
            Contract.Ensures(o is int);

            //ErrorInReturn
            Contract.Ensures(Contract.Result<int>() > 5);

            return o as int? ?? 0;
        }
        
        static void Main(string[] args)
        {
            Contract.ContractFailed += (sender, e) =>
            {
                Console.WriteLine(e.Message);
                e.SetHandled();
            };

            TestMethod();
        }
    }
}
