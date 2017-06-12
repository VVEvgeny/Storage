using System;
using System.Reflection;

namespace Principles._4.InterfaceSegregationPrinciple
{
    public interface IInterfaceSegregationPrincipleDo1
    {
        void Do();
    }

    public interface IInterfaceSegregationPrincipleDo2
    {
        void Do();
    }


    public class InterfaceSegregationPrinciple1 : IInterfaceSegregationPrincipleDo1
    {
        public void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
    }

    public class InterfaceSegregationPrinciple2 : IInterfaceSegregationPrincipleDo2
    {
        public void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
    }

    public class InterfaceSegregationPrinciple3 : IInterfaceSegregationPrincipleDo1, IInterfaceSegregationPrincipleDo2
    {
        void IInterfaceSegregationPrincipleDo1.Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }

        void IInterfaceSegregationPrincipleDo2.Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
    }
}
