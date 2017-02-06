using System;
using System.Collections.Generic;
using System.Linq;

namespace Structural
{
    public abstract class AClass
    {
        protected string Name { get; set; }

        public AClass(string name)
        {
            Name = name;
        }

        public virtual string Get()
        {
            return Name;
        }
        public abstract void Add(AClass c);

        public abstract void Remove(AClass c);
    }

    public class CompositeClass : AClass
    {
        public CompositeClass(string name) : base(name)
        {

        }

        public override void Add(AClass c)
        {
            throw new NotImplementedException();
        }

        public override void Remove(AClass c)
        {
            throw new NotImplementedException();
        }
    }

    public class Class2 : AClass
    {
        public Class2(string name) : base(name)
        {

        }

        public override void Add(AClass c)
        {
            throw new NotImplementedException();
        }

        public override void Remove(AClass c)
        {
            throw new NotImplementedException();
        }
    }

    public class Composite : AClass
    {
        private readonly List<AClass> _items = new List<AClass>();

        public Composite(string name) : base(name)
        {

        }

        public Composite() : base(nameof(Composite))
        {

        }

        public override void Add(AClass item)
        {
            _items.Add(item);
        }

        public override void Remove(AClass item)
        {
            _items.Remove(item);
        }
        public override string Get()
        {
            return _items.Aggregate(string.Empty, (current, item) => current + item.Get() + " ");
        }
    }
}