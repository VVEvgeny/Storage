namespace Structural
{
    public interface IBaseClass
    {
        string Description();
        int Count();
    }

    public class BaseClass : IBaseClass
    {
        public virtual string Description()
        {
            return "base";
        }

        public virtual int Count()
        {
            return 1;
        }
    }

    public class Derived1 : BaseClass
    {
        public override string Description()
        {
            return "derived1";
        }
    }
    public class Derived2 : BaseClass
    {
        public override string Description()
        {
            return "derived2";
        }
    }

    public interface IUnDecorate
    {
        IBaseClass UnDecorate();
    }
    public class Decorator1 : BaseClass, IUnDecorate
    {
        private readonly IBaseClass _iBaseClass;
        public Decorator1(IBaseClass iBaseClass)
        {
            _iBaseClass = iBaseClass;
        }
        public override int Count()
        {
            return _iBaseClass.Count() + 1;
        }

        public override string Description()
        {
            return _iBaseClass.Description() + " deco1";
        }

        public IBaseClass UnDecorate()
        {
            return _iBaseClass;
        }
    }
    public class Decorator2 : BaseClass, IUnDecorate
    {
        private readonly IBaseClass _iBaseClass;
        public Decorator2(IBaseClass iBaseClass)
        {
            _iBaseClass = iBaseClass;
        }
        public override int Count()
        {
            return _iBaseClass.Count() + 1;
        }

        public override string Description()
        {
            return _iBaseClass.Description() + " deco2";
        }

        public IBaseClass UnDecorate()
        {
            return _iBaseClass;
        }
    }
}