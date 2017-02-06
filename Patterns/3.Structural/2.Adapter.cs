using System;

namespace Structural
{
    public class ClassType1
    {
        public virtual string Name => GetType().Name;
    }

    public class ClassType2
    {
        public virtual string MyName => GetType().Name;
    }

    public class Adapter : ClassType1
    {
        private readonly ClassType2 _classType2;

        public Adapter(ClassType2 classType2)
        {
            _classType2 = classType2;
        }

        public override string Name => _classType2.MyName;
    }
}