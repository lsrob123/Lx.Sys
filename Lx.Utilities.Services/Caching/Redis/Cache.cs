using System.Configuration;
using System.Threading;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Caching.Redis.Config;
using StackExchange.Redis;

namespace Lx.Utilities.Services.Caching.Redis
{
    public class Cache : CacheDatabase //, ICacheWithTransactions
    {
        protected static readonly object LockForAddingEndpoints = new object();

        public Cache(ISerializer serializer = null, ICacheConfig configuration = null) : base(
            () =>
            {
                if (configuration == null)
                    configuration = CacheConfigSectionHandler.GetConfig();

                if (configuration == null)
                    throw new ConfigurationErrorsException(
                        "Unable to locate <redisCacheClient> section into your configuration file.");

                return configuration;
            },
            config =>
            {
                var options = new ConfigurationOptions
                {
                    Ssl = config.Ssl,
                    AllowAdmin = config.AllowAdmin,
                    Password = config.Password,
                    AbortOnConnectFail = false
                };
                foreach (CacheHost redisHost in config.RedisHosts)
                {
                    Monitor.Enter(LockForAddingEndpoints);
                    try
                    {
                        options.EndPoints.Add(redisHost.Host, redisHost.CachePort);
                    }
                    finally
                    {
                        Monitor.Exit(LockForAddingEndpoints);
                    }
                }

                var connectionMultiplexer = ConnectionMultiplexer.Connect(options);
                return connectionMultiplexer;
            },
            (config, connectionMultiplexer) =>
            {
                var database = connectionMultiplexer.GetDatabase(config.Database);
                return database;
            }, serializer)
        {
        }

        public Cache(string connectionString, int database = 0, ISerializer serializer = null) : base(null,
            config => ConnectionMultiplexer.Connect(connectionString),
            (config, connectionMultiplexer) => connectionMultiplexer.GetDatabase(database), serializer)
        {
        }
    }
}