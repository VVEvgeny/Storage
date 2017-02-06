using System;
using System.Reflection;

namespace Generating
{
    public class Class
    {
        public void Do1()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        public void Do2()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        public void Action()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
    }

    public class BuilderClass
    {
        private readonly Class _class = new Class();

        private enum States
        {
            None,
            Do1Complete,
            Do2Complete
        }

        private States _state = States.None;

        public BuilderClass Do1()
        {
            if (_state != States.None) throw new InvalidOperationException("Do1 already called");
            _class.Do1();
            _state = States.Do1Complete;
            return this;
        }
        public BuilderClass Do2()
        {
            if (_state != States.Do1Complete) throw new InvalidOperationException("Do2 already called, or not called Do1");
            _class.Do2();
            _state = States.Do2Complete;
            return this;
        }
        public Class Action()
        {
            if (_state != States.Do2Complete) throw new InvalidOperationException("Do2 not called");
            _class.Action();
            return _class;
        }
    }
}