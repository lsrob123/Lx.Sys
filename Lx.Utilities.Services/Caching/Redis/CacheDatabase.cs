using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Services.Caching.Redis.Config;
using Lx.Utilities.Services.Serialization;
using StackExchange.Redis;

namespace Lx.Utilities.Services.Caching.Redis
{
    public class CacheDatabase : ICacheWithHashes
    {
        protected readonly ICacheConfig Config;
        protected readonly ConnectionMultiplexer ConnectionMultiplexer;
        protected readonly IDatabase Database;
        protected readonly ISerializer Serializer;

        public CacheDatabase(Func<ICacheConfig> configFactory,
            Func<ICacheConfig, ConnectionMultiplexer> connectionMultiplexerFactory,
            Func<ICacheConfig, ConnectionMultiplexer, IDatabase> databaseFactory, ISerializer serializer = null)
            : this(serializer)
        {
            if (configFactory != null)
                Config = configFactory();

            ConnectionMultiplexer = connectionMultiplexerFactory(Config);
            Database = databaseFactory(Config, ConnectionMultiplexer);
        }

        public CacheDatabase(IDatabase database, ISerializer serializer = null) : this(serializer)
        {
            Database = database;
        }

        public CacheDatabase(ISerializer serializer = null)
        {
            Serializer = serializer ?? new JsonSerializer();
        }

        public bool Exists(string cacheKey)
        {
            return Database.KeyExists(cacheKey);
        }

        public async Task<bool> RemoveCachedItemAsync(string cacheKey)
        {
            var result = await Database.KeyDeleteAsync(cacheKey);
            return result;
        }

        public T GetCachedItem<T>(string cacheKey)
        {
            var redisValue = Database.StringGet(cacheKey).ToString();
            return string.IsNullOrWhiteSpace(redisValue)
                ? default(T)
                : Serializer.Deserialize<T>(redisValue);
        }

        public object GetCachedItem(string cacheKey, Type type)
        {
            var redisValue = Database.StringGet(cacheKey).ToString();
            return string.IsNullOrWhiteSpace(redisValue)
                ? null
                : Serializer.Deserialize(redisValue, type);
        }

        public async Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, TimeSpan expiration)
        {
            var cachedString = Serializer.Serialize(cachedItem);
            var result = await Database.StringSetAsync(cacheKey, cachedString, expiration);
            return result;
        }

        public async Task HashSetAsync(string hashKey, IDictionary<string, string> nameValues)
        {
            await Database.HashSetAsync(hashKey, nameValues.Select(x => new HashEntry(x.Key, x.Value)).ToArray());
        }

        public async Task HashDeleteAsync(string hashKey, string fieldName = null)
        {
            if (fieldName != null)
            {
                await Database.HashDeleteAsync(hashKey, fieldName);
                return;
            }

            var entries = HashGet(hashKey);
            foreach (var entry in entries)
                await Database.HashDeleteAsync(hashKey, entry.Key);
        }

        public async Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem)
        {
            var cachedString = Serializer.Serialize(cachedItem);
            var result = await Database.StringSetAsync(cacheKey, cachedString);
            return result;
        }

        public void Dispose()
        {
            ConnectionMultiplexer.Dispose();
        }

        public async Task HashSetAsync(string hashKey, string itemName, string itemValue)
        {
            var pair = new KeyValuePair<string, string>(itemName, itemValue);
            await HashSetAsync(hashKey, pair);
        }

        public async Task HashSetAsync(string hashKey, params KeyValuePair<string, string>[] nameValues)
        {
            await Database.HashSetAsync(hashKey, nameValues.Select(x => new HashEntry(x.Key, x.Value)).ToArray());
        }

        public void FlushDb()
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
                Database.Multiplexer.GetServer(endpoint).FlushDatabase(Database.Database);
        }

        public IDictionary<string, string> HashGet(string hashKey, IEnumerable<string> nameEnumerable = null,
            params string[] individuallySuppliedNames)
        {
            List<string> names = null;
            if (nameEnumerable != null || individuallySuppliedNames != null && individuallySuppliedNames.Any())
            {
                names = new List<string>();
                if (nameEnumerable != null)
                    names.AddRange(nameEnumerable);

                if (individuallySuppliedNames != null)
                    names.AddRange(individuallySuppliedNames);
            }

            var fields = new Dictionary<string, string>();

            if (names == null)
            {
                var entries = Database.HashGetAll(hashKey);
                foreach (var entry in entries)
                    fields.Add(entry.Name, entry.Value);
            }
            else
            {
                foreach (var name in names)
                {
                    var value = Database.HashGet(hashKey, name);
                    if (value.HasValue)
                        fields.Add(name, value);
                }
            }

            return fields;
        }

        public string HashGet(string hashKey, string fieldName)
        {
            var value = Database.HashGet(hashKey, fieldName);
            if (value.HasValue)
                return value;

            return null;
        }

        public void RemoveAllCachedItems(ICollection<string> cacheKeys)
        {
            foreach (var cacheKey in cacheKeys)
                RemoveCachedItemAsync(cacheKey).Wait();
        }

        public IDictionary<string, T> GetAllCachedItems<T>(ICollection<string> cacheKeys)
        {
            var redisKeys = cacheKeys.Select(x => (RedisKey) x).ToArray();
            var result = Database.StringGet(redisKeys);
            return redisKeys.ToDictionary(key => (string) key, key =>
            {
                {
                    var index = Array.IndexOf(redisKeys, key);
                    var value = result[index];
                    return value == RedisValue.Null ? default(T) : Serializer.Deserialize<T>(result[index]);
                }
            });
        }

        public IDictionary<string, T> GetAllCachedItems<T>()
        {
            var cacheKeys = GetKeys();
            var redisKeys = cacheKeys.Select(x => (RedisKey) x).ToArray();
            var result = Database.StringGet(redisKeys);
            return redisKeys.ToDictionary(key => (string) key, key =>
            {
                {
                    var index = Array.IndexOf(redisKeys, key);
                    var value = result[index];
                    return value == RedisValue.Null ? default(T) : Serializer.Deserialize<T>(result[index]);
                }
            });
        }

        public async Task<bool> SetCachedItemAsync<T>(string cacheKey, T cachedItem, DateTimeOffset expiresAt)
            where T : class
        {
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);
            return await SetCachedItemAsync(cacheKey, cachedItem, expiration);
        }

        public bool SetCachedItems<T>(IList<Tuple<string, T>> cachedItems)
        {
            var redisKeyValueDictionary =
                cachedItems.ToDictionary<Tuple<string, T>, RedisKey, RedisValue>(item => item.Item1,
                    item => Serializer.Serialize(item.Item2));

            return Database.StringSet(redisKeyValueDictionary.ToArray());
        }

        public Dictionary<string, string> GetInfo()
        {
            var redisInfo = Database.ScriptEvaluate("return redis.call('INFO')").ToString();

            return ParseRedisInfo(redisInfo);
        }

        public ICollection<string> GetKeys()
        {
            var keys = (string[]) Database.ScriptEvaluate("return redis.call('KEYS','*')");

            return keys;
        }

        public void SaveDb(SaveType saveType)
        {
            var endPoints = Database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endPoints)
                Database.Multiplexer.GetServer(endpoint).Save(saveType);
        }

        protected static Dictionary<string, string> ParseRedisInfo(string redisInfo)
        {
            var strArr = redisInfo.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dict = new Dictionary<string, string>();
            foreach (var str in strArr)
            {
                if (string.IsNullOrEmpty(str) || str[0] == '#')
                    continue;

                var idx = str.IndexOf(':');
                if (idx <= 0)
                    continue;

                var key = str.Substring(0, idx);
                var infoValue = str.Substring(idx + 1).Trim();
                dict.Add(key, infoValue);
            }
            return dict;
        }
    }
}