using System;
using System.Reflection;

namespace Principles._1.SingleResponsibilityPrinciple
{
    public abstract class ISingleResponsibilityPrinciple
    {
        public void Do(string text)
        {
            Console.WriteLine(GetType().Name + "::" + text);
        }
    }

    public class SingleResponsibilityPrincipleDo1 : ISingleResponsibilityPrinciple
    {

    }
    public class SingleResponsibilityPrincipleDo2 : ISingleResponsibilityPrinciple
    {

    }

    public class SingleResponsibilityPrinciple
    {
        public void PreDo()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }

        public void Do(ISingleResponsibilityPrinciple iSingleResponsibilityPrinciple)
        {
            iSingleResponsibilityPrinciple.Do(GetType().Name + " " + MethodBase.GetCurrentMethod().Name);
        }
    }
}
