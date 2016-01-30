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
    class ProductsRedisCache : ICategoriesCache<Product>
    {
        private ConnectionMultiplexer redisConnection;
        string prefix = "Cache_Products";
        DataContractSerializer serializer = new DataContractSerializer(
            typeof(IEnumerable<Product>));

        public ProductsRedisCache(string hostName)
        {
            redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Product> Get(string forUser)
        {
            var db = redisConnection.GetDatabase();
            byte[] s = db.StringGet(prefix + forUser);
            if (s == null)
                return null;

            return (IEnumerable<Product>)serializer
                .ReadObject(new MemoryStream(s));
        }

        public void Set(string forUser, IEnumerable<Product> categories)
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
