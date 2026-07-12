using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepo;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepo = cacheRepository;
        }
        public Task<string?> GetAsync(string key, CancellationToken ct = default)
         => _cacheRepo.GetCacheValueAsync(key, ct);

        public Task SetAsync(string key, string value, TimeSpan expiration, CancellationToken ct = default)
        {
          var json = JsonSerializer.Serialize(value , new JsonSerializerOptions()
          { 
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          });
            return _cacheRepo.SetAsync(key, json, expiration, ct);
        }
    }
}
