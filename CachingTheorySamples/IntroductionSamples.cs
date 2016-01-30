using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;

namespace CachingTheorySamples
{
	public class UserProfile
	{ }

	[TestClass]
	public class IntroductionSamples
	{
		[TestMethod]
		public void SimpleUsageOfMemoryCache()
		{
			ObjectCache cache = MemoryCache.Default;

			string key = "123"; // Profile Id

			UserProfile profile = cache.Get(key) as UserProfile;
			if (profile == null)
			{
				// Get profile from DB

				cache.Set(key, profile, ObjectCache.InfiniteAbsoluteExpiration);
			}
			
			// Use profile
		}
	}
}
