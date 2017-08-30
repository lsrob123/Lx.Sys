using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Serialization;

namespace Lx.Identity.ID3.Stores
{
    public class Id3AuthorizationCodeStore : IAuthorizationCodeStore
    {
        protected static readonly string KeyToAuthorizationCodeHash = typeof(AuthorizationCode).FullName;
        protected readonly ICacheFactory CacheFactory;
        protected readonly ISerializer Serializer;

        public Id3AuthorizationCodeStore(ICacheFactory cacheFactory, ISerializer serializer)
        {
            CacheFactory = cacheFactory;
            Serializer = serializer;
        }

        public async Task StoreAsync(string key, AuthorizationCode value)
        {
            var hashKey = KeyToAuthorizationCodeHash + value.SubjectId;
            var json = Serializer.Serialize(value);

            CacheFactory.Execute(cache =>
            {
                cache.HashSetAsync(hashKey, key, json).Wait();
                cache.SetCachedItemAsync(GetCacheKey(key), json, TimeSpan.FromDays(1)).Wait();
            });

            await Task.CompletedTask;
        }

        public async Task<AuthorizationCode> GetAsync(string key)
        {
            var json = CacheFactory.Get(cache => cache.GetCachedItem<string>(GetCacheKey(key)));
            if (string.IsNullOrWhiteSpace(json))
                return null;

            var code = Serializer.Deserialize<AuthorizationCode>(json);
            return await Task.FromResult(code);
        }

        public async Task RemoveAsync(string key)
        {
            var code = await GetAsync(key);
            CacheFactory.Execute(cache =>
            {
                cache.RemoveCachedItemAsync(GetCacheKey(key)).Wait();
                cache.HashDeleteAsync(KeyToAuthorizationCodeHash + code.SubjectId, key).Wait();
            });
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            var entries = CacheFactory.Get(cache => cache.HashGet(KeyToAuthorizationCodeHash + subject).Values);
            var codes = entries.Select(x => Serializer.Deserialize<AuthorizationCode>(x)).ToList();
            return await Task.FromResult(codes);
        }

        public async Task RevokeAsync(string subject, string client)
        {
            var allEntries = CacheFactory.Get(cache => cache.HashGet(KeyToAuthorizationCodeHash + subject));

            foreach (var entry in allEntries)
            {
                var code = Serializer.Deserialize<AuthorizationCode>(entry.Value);
                if (code.ClientId != client)
                    continue;

                CacheFactory.Execute(cache =>
                {
                    cache.RemoveCachedItemAsync(GetCacheKey(entry.Key)).Wait();
                    cache.HashDeleteAsync(KeyToAuthorizationCodeHash + code.SubjectId, entry.Key).Wait();
                });
            }

            await Task.CompletedTask;
        }

        private static string GetCacheKey(string key)
        {
            return CacheKeyHelper.GetCacheKey<AuthorizationCode>(key);
        }
    }
}