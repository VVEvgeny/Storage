using System;
using System.Reflection;

namespace Principles._2.OpenClosedPrinciple
{
    public abstract class IOpenClosedPrinciple
    {
        public void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
    }

    public class OpenClosedPrincipleDo1:IOpenClosedPrinciple
    {

    }

    public class OpenClosedPrincipleDo2 : IOpenClosedPrinciple
    { 

    }

    public class OpenClosedPrinciple
    {
        private readonly IOpenClosedPrinciple _iOpenClosedPrinciple;
        public OpenClosedPrinciple(IOpenClosedPrinciple iOpenClosedPrinciple)
        {
            this._iOpenClosedPrinciple = iOpenClosedPrinciple;
        }

        private void PreDo()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
        private void Do()
        {
            _iOpenClosedPrinciple.Do();
        }

        public void AllDo()
        {
            PreDo();
            Do();
        }
    }
}
