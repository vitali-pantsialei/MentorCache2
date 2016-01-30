using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    public class CustomersManager
    {
        private ICategoriesCache<Customer> cache;

        public CustomersManager(ICategoriesCache<Customer> cache)
		{
			this.cache = cache;
		}

        public IEnumerable<Customer> GetCustomers()
		{
			var user = Thread.CurrentPrincipal.Identity.Name;
			var customers = cache.Get(user);

            if (customers == null)
			{
				Console.WriteLine("From DB");

				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					customers = dbContext.Customers.ToList();
                    cache.Set(user, customers);
				}
			}

            return customers;
		}
    }
}
