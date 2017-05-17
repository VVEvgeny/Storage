namespace Generating
{
    public abstract class Property
    {
        public string Name => GetType().Name;
    }
    public abstract class AbstractClassA: Property { }
    public abstract class AbstractClassB : Property { }

    public class ClassA1 : AbstractClassA { }
    public class ClassA2 : AbstractClassA { }
    public class ClassB1 : AbstractClassB { }
    public class ClassB2 : AbstractClassB { }

    public abstract class AbstractFactory
    {
        public abstract AbstractClassA CreateClassA();
        public abstract AbstractClassB CreateClassB();
    }

    public class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractClassA CreateClassA()
        {
            return new ClassA1();
        }

        public override AbstractClassB CreateClassB()
        {
            return new ClassB1();
        }
    }

    public class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractClassA CreateClassA()
        {
            return new ClassA2();
        }

        public override AbstractClassB CreateClassB()
        {
            return new ClassB2();
        }
    }

}
