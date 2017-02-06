using System;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Generating
{
    public abstract class aClass
    {
        protected internal void DoAction()
        {
            Console.WriteLine(Action);
        }

        public string Action => GetType().Name;
    }

    public class ClassDerived1 : aClass
    {

    }
    public class ClassDerived2 : aClass
    {

    }

    public static class ClassFactory
    {
        public static T Create<T>() where T : aClass, new()
        {
            try
            {
                var t = new T();
                t.DoAction();
                return t;
            }
            catch (TargetInvocationException tie)
            {
                var edi = ExceptionDispatchInfo.Capture(tie.InnerException);
                edi.Throw();
                return default(T);
            }
        }
    }

}