using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeSpan = null, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(basket);
            var success = await _database.StringSetAsync(basket.Id, json, timeSpan??TimeSpan.FromDays(30));
            return success ? basket : null;
        }

        public Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
         => _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
