using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;
using System.Data.SqlClient;

namespace CachingTheorySamples
{
	[TestClass]
	public class CacheInvalidationSample
	{
		ObjectCache cache = MemoryCache.Default;

		[TestMethod]
		public void ExpirationTime()
		{
			string key = "some_key";

			UserProfile profile = new UserProfile();

			cache.Add(key, profile, 
				new DateTimeOffset(DateTime.UtcNow.AddMinutes(10)));
		}

		[TestMethod]
		public void MonitorSample()
		{
			string key = "some_key";

			UserProfile profile = new UserProfile();

			var policy = new CacheItemPolicy();
			policy.ChangeMonitors.Add(
				new SqlChangeMonitor(
					new SqlDependency(
						new SqlCommand("select * From dbo.UserProfile"))));

			cache.Add(key, profile, policy);
		}


	}
}
