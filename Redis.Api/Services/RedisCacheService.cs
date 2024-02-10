using StackExchange.Redis;

namespace Redis.Api.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connection;

        public RedisCacheService(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }
        public async Task<string> GetCacheValueAsync(string key)
        {
            var db = _connection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            var db = _connection.GetDatabase();
            await db.StringSetAsync(key, value);
        }
    }
}
