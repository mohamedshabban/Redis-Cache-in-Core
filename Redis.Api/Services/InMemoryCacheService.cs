using Microsoft.Extensions.Caching.Memory;

namespace Redis.Api.Services
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache;
        public InMemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        public async Task<string> GetCacheValueAsync(string key)
        {
            return await Task.FromResult(_cache.Get<string>(key));
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            await Task.CompletedTask;
        }
    }
}
