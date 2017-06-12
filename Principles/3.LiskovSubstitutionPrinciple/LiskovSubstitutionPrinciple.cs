using System;
using System.Reflection;

namespace Principles._3.LiskovSubstitutionPrinciple
{
    public class LiskovSubstitutionPrincipleBase
    {
        public virtual void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name);
        }
    }

    public class LiskovSubstitutionPrincipleType1: LiskovSubstitutionPrincipleBase
    {
        public override void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name+"+");
            base.Do();
        }
    }

    public class LiskovSubstitutionPrincipleType11 : LiskovSubstitutionPrincipleType1
    {
        public override void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name + "++");
            base.Do();
        }
    }

    public class LiskovSubstitutionPrincipleType2 : LiskovSubstitutionPrincipleBase
    {
        public override void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name + "+");
            base.Do();
        }
    }

    public class LiskovSubstitutionPrincipleType22 : LiskovSubstitutionPrincipleType2
    {
        public override void Do()
        {
            Console.WriteLine(GetType().Name + "::" + MethodBase.GetCurrentMethod().Name + "++");
            base.Do();
        }
    }
}
