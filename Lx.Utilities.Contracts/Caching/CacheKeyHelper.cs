namespace Lx.Utilities.Contracts.Caching
{
    public static class CacheKeyHelper
    {
        public static string GetCacheKey<T>(object keyword, bool enforceLowercaseToSuffix = true)
        {
            var cacheKey = $"{typeof(T).FullName}";
            if (keyword != null)
            {
                var suffixText = $"{keyword}".Trim();
                cacheKey = $"{cacheKey}_{suffixText}";
            }

            if (enforceLowercaseToSuffix)
                cacheKey = cacheKey.ToLower();

            return cacheKey;
        }
    }
}