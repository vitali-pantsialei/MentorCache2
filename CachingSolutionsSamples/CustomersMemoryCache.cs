using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    internal class CustomersMemoryCache : ICategoriesCache<Customer>
    {
        ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Customers";

        public IEnumerable<Customer> Get(string forUser)
        {
            return (IEnumerable<Customer>)cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Customer> categories)
        {
            //policy 10 minutes
            cache.Set(prefix + forUser, categories, new DateTimeOffset(DateTime.UtcNow.AddMinutes(10)));
        }
    }
}
