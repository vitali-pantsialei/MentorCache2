using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CachingSolutionsSamples
{
    internal class ProductsMemoryCache : ICategoriesCache<Product>
    {
        ObjectCache cache = MemoryCache.Default;
        string prefix = "Cache_Products";

        public IEnumerable<Product> Get(string forUser)
        {
            return (IEnumerable<Product>)cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Product> categories)
        {
            //policy on update of products table
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(
                new SqlChangeMonitor(
                    new SqlDependency(
                        new SqlCommand("select * from dbo.Products"))));
            cache.Set(prefix + forUser, categories, policy);
        }
    }
}
