using E_Commerce.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetCacheValueAsync(string key, CancellationToken ct = default)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public Task SetAsync(string key, string value, TimeSpan expiration, CancellationToken ct = default)
        => _database.StringSetAsync(key, value, expiration);
    }
}
