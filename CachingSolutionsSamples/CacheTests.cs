using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
			var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

        [TestMethod]
        public void MemoryCacheCustomers()
        {
            var customerManager = new CustomersManager(new CustomersMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customerManager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCacheCustomers()
        {
            var customerManager = new CustomersManager(new CustomersRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(customerManager.GetCustomers().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void MemoryCacheProducts()
        {
            var productManager = new ProductsManager(new ProductsMemoryCache());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(productManager.GetProducts().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void RedisCacheProducts()
        {
            var productManager = new ProductsManager(new ProductsRedisCache("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(productManager.GetProducts().Count());
                Thread.Sleep(100);
            }
        }
	}
}
