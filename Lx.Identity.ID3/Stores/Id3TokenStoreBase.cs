using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Serialization;

namespace Lx.Identity.ID3.Stores
{
    public abstract class Id3TokenStoreBase<T> : ITransientDataRepository<T> where T : class, ITokenMetadata
    {
        protected readonly ICacheFactory CacheFactory;
        protected readonly IClaimRelatedJsonDeserializer ClaimRelatedJsonDeserializer;
        protected readonly ILogger Logger;
        protected readonly ISerializer Serializer;
        protected readonly string TokenHashKey = typeof(T).FullName;

        protected readonly string TokenSubjectClientHashKey = typeof(T).FullName + "Subject" +
                                                              typeof(Client).Name + "Hash";

        protected readonly string TokenSubjectHashKey = typeof(T).FullName + "SubjectHash";

        protected Id3TokenStoreBase(ICacheFactory cacheFactory, ISerializer serializer, ILogger logger,
            IClaimRelatedJsonDeserializer claimRelatedJsonDeserializer)
        {
            CacheFactory = cacheFactory;
            Serializer = serializer;
            Logger = logger;
            ClaimRelatedJsonDeserializer = claimRelatedJsonDeserializer;
        }

        protected string FieldNamePrefix => GetType().Name;

        public async Task StoreAsync(string key, T value)
        {
            var cachedValue = SerializeToken(value);

            var fieldName = FieldNamePrefix + key;

            CacheFactory.Execute(cache =>
            {
                cache.HashSetAsync(TokenHashKey, new KeyValuePair<string, string>(fieldName, cachedValue)).Wait();
                cache.HashSetAsync(GetTokenSubjectHashKey(value.SubjectId),
                    new KeyValuePair<string, string>(fieldName, key)).Wait();
                cache.HashSetAsync(GetTokenSubjectClientHashKey(value.SubjectId, value.ClientId),
                    new KeyValuePair<string, string>(fieldName, key)).Wait();
            });

            await Task.CompletedTask;
        }

        public async Task<T> GetAsync(string key)
        {
            var fieldName = FieldNamePrefix + key;

            var cachedValue = CacheFactory.Get(cache => cache.HashGet(TokenHashKey, fieldName));
            var value = cachedValue == null ? null : DeserializeToken(cachedValue);
            return await Task.FromResult(value);
        }

        public async Task RemoveAsync(string key)
        {
            var fieldName = FieldNamePrefix + key;

            CacheFactory.Execute(cache => cache.HashDeleteAsync(TokenHashKey, fieldName).Wait());
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            using (var cache = CacheFactory.NewDisposableCache())
            {
                var fieldNames = cache.HashGet(GetTokenSubjectHashKey(subject)).Keys;
                var tokens = (from fieldName in fieldNames
                        select cache.HashGet(TokenHashKey, fieldName)
                        into tokenValue
                        where tokenValue != null
                        select DeserializeToken(tokenValue))
                    .ToList();
                return await Task.FromResult(tokens);
            }
        }

        public async Task RevokeAsync(string subject, string client)
        {
            using (var cache = CacheFactory.NewDisposableCache())
            {
                var fieldNames = cache.HashGet(GetTokenSubjectClientHashKey(subject, client)).Keys;

                foreach (var fieldName in fieldNames)
                {
                    await cache.HashDeleteAsync(TokenHashKey, fieldName);
                    await cache.HashDeleteAsync(GetTokenSubjectHashKey(subject), fieldName);
                    await cache.HashDeleteAsync(GetTokenSubjectClientHashKey(subject, client), fieldName);
                }
            }
        }

        protected string SerializeToken(T token)
        {
            var json = Serializer.Serialize(token);
            return json;
        }

        protected T DeserializeToken(string cachedValue)
        {
            return ClaimRelatedJsonDeserializer.Deserialize<T>(cachedValue);
        }

        protected string GetTokenSubjectHashKey(string subjectId)
        {
            return TokenSubjectHashKey + subjectId;
        }

        protected string GetTokenSubjectClientHashKey(string subjectId, string clientId)
        {
            return TokenSubjectClientHashKey + subjectId + clientId;
        }
    }
}