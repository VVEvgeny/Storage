using System;
using System.Reflection;

namespace Principles._5.DependencyInversionPrinciple
{
    public abstract class IDependencyInversionPrinciple
    {
        public void Do(string text)
        {
            Console.WriteLine(GetType().Name + "::" + text);
        }
    }

    public class IDependencyInversionPrinciple1 : IDependencyInversionPrinciple
    {
        
    }
    public class IDependencyInversionPrinciple2 : IDependencyInversionPrinciple
    {

    }


    public class DependencyInversionPrinciple
    {
        public void Do(IDependencyInversionPrinciple iDependencyInversionPrinciple)
        {
            iDependencyInversionPrinciple.Do(GetType().Name + " " + MethodBase.GetCurrentMethod().Name);
        }
    }
}
