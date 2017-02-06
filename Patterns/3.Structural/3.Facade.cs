namespace Structural
{
    public class ClassAction1
    {
        public string DoAction => GetType().Name;
    }
    public class ClassAction2
    {
        public string DoAction => GetType().Name;
    }
    public class ClassAction3
    {
        public string DoAction => GetType().Name;
    }

    public class Facade
    {
        private ClassAction1 _classAction1;
        private ClassAction2 _classAction2;
        private ClassAction3 _classAction3;

        public Facade()
        {
            _classAction1 = new ClassAction1();
            _classAction2 = new ClassAction2();
            _classAction3 = new ClassAction3();
        }

        public string DoAction => _classAction1.DoAction + _classAction2.DoAction + _classAction3.DoAction;
    }
}