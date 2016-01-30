using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingTheorySamples
{
	public interface IProfileCache
	{
		UserProfile Get(int userId, Guid subscriptionId);

		void Set(int userId, Guid subscriptionId, UserProfile profile);
	}

	[TestClass]
	public class StructuredWrapperSample
	{
		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
