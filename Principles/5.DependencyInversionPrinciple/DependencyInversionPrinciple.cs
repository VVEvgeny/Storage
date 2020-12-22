using System;
using System.Reflection;

namespace Principles._5.DependencyInversionPrinciple
{
    public abstract class IDependencyInversionPrinciple
    {
        public void Do(string text)
        {
            Console.WriteLine(GetType().Name + "::" + text);
        }
    }

    public class IDependencyInversionPrinciple1 : IDependencyInversionPrinciple
    {
        
    }
    public class IDependencyInversionPrinciple2 : IDependencyInversionPrinciple
    {

    }


    public class DependencyInversionPrinciple
    {
        public void Do(IDependencyInversionPrinciple iDependencyInversionPrinciple)
        {
            iDependencyInversionPrinciple.Do(GetType().Name + " " + MethodBase.GetCurrentMethod().Name);
        }
    }
}
public interface IItemService
{
    void Save(Item item);
}

public class ItemService : IItemService, IDisposable
{
    // ConnectionException is possible
    public void Save(Item item)
    {
        // some impl. }
        public void Dispose()
        {
            //some impl.}

            ~ItemService(){
                Dispose();
            }
        }

// the code above is sitting in a DLL
// save 3 times with connection exception - consider this as failure.
// we are writing PRODUCTION code
    public class ClassForSaving
    {
        private IItemService iItemService;

        public ClassForSaving(IItemService _iItemService)
        {
            iItemService = _iItemService;
        }

        public void Save(Item item)
        {
            int repeats = 3;
            while (repeats > 0)
            {
                try
                {
                    using (var itemService = new iItemService())
                    {
                        itemService.Save(item);
                        repeats = 0;
                    }
                }
                catch (ConnectionExc ex)
                {
                    repeats--;
                    if (repeats == 0)
                        //throw;
                        throw ex;
                }
            }
        }
    }
}