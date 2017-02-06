using vvevgeny_storage;

namespace Generating
{
    public sealed class FactoryMethodTestView : ITestView
    {
        public void Run()
        {
            ClassFactory.Create<ClassDerived1>();
            ClassFactory.Create<ClassDerived2>();
        }
    }
}