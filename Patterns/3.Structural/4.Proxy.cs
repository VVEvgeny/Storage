namespace Structural
{
    public interface IClass
    {
        string Action();
    }

    public class Class : IClass
    {
        public string Action()
        {
            return GetType().Name;
        }
    }

    public class Proxy : IClass
    {
        private readonly Class _proxyClass = new Class();

        public string Action()
        {
            return GetType().Name + _proxyClass.Action();
        }
    }
}