using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.Cache
{
    public class RedisCache : ICacheProvider<Dictionary<string, string>>, IDisposable
    {
        private string config = "localhost:6379"; // TODO move to config
        private ConnectionMultiplexer connection;
        private IDatabase database;
        private string groupName = "HolidayOptimizer";

        public RedisCache()
        {            
            connection = ConnectionMultiplexer.Connect(new ConfigurationOptions { AbortOnConnectFail = false, ConnectRetry = 5, EndPoints = { config } });
            database = connection.GetDatabase(-1);
        }

        public void AddOrUpdate(Dictionary<string, string> data, string key)
        {
            database.HashSet($"{groupName}:{key}", data.Select(d => new HashEntry(d.Key, new RedisValue(d.Value))).ToArray());
        }

        public Dictionary<string, string> GetCached(string key)
        {
            return database.HashGetAll($"{groupName}:{key}").ToStringDictionary();
        }

        public bool KeyExists(string key)
        {
            return database.KeyExists($"{groupName}:{key}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
            }
        }
    }
}
