namespace Generating
{
    public abstract class PrototypeClass
    {
        public class StringRef
        {
            public string Str;
        }
        public StringRef Id { get; set; }
        public string Name => GetType().Name + " "+ Id.Str;

        protected PrototypeClass(string id)
        {
            Id = new StringRef {Str = id};
        }
        public abstract PrototypeClass Clone();

        public PrototypeClass Clone2()
        {
            return MemberwiseClone() as PrototypeClass;
        }
    }
    public class ConcretePrototype1 : PrototypeClass
    {
        public ConcretePrototype1(string id) : base(id)
        {
            
        }
        public override PrototypeClass Clone()
        {
            return new ConcretePrototype1(Id.Str);
        }
    }

    public class ConcretePrototype2 : PrototypeClass
    {
        public ConcretePrototype2(string id) : base(id)
        {
            
        }
        public override PrototypeClass Clone()
        {
            return new ConcretePrototype2(Id.Str);
        }
    }
}