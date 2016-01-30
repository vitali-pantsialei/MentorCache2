using NorthwindLibrary;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
    class CustomersRedisCache : ICategoriesCache<Customer>
    {
        private ConnectionMultiplexer redisConnection;
        string prefix = "Cache_Customers";
        DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<Customer>));

        public CustomersRedisCache(string hostName)
        {
            redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Customer> Get(string forUser)
        {
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Customer>)serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Customer> categories)
        {
            var db = redisConnection.GetDatabase();
            var key = prefix + forUser;

            if (categories == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                serializer.WriteObject(stream, categories);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
