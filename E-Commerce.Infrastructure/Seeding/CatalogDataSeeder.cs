using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Seeding
{
    public class CatalogDataSeeder(StoreDbContext context, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task DataSeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any()) await context.Database.MigrateAsync(ct);

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                await SeedIfEmptyAsync<ProductBrand>(seedRoot, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType>(seedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product>(seedRoot, "products.json", ct);

                await context.SaveChangesAsync(ct);
            }
            catch (Exception ex) {

                logger.LogError(ex, "Data seeding failed");
                throw;
            }

        }


        private async Task SeedIfEmptyAsync<T>(string root , string fileName , CancellationToken ct = default) where T : class
        {
            if (await context.Set<T>().AnyAsync(ct)) return;

            var path = Path.Combine(root, fileName);

            if (!File.Exists(path))
            {
                logger.LogWarning("Seed file doesn't exist:{Path}" , path);
                return;
            }

            await using var stream =File.OpenRead(path);
            
            var items = await JsonSerializer.DeserializeAsync<List<T>>(stream , new JsonSerializerOptions { PropertyNameCaseInsensitive = true} , ct);

            if(items?.Count > 0) await context.Set<T>().AddRangeAsync(items, ct);

        }
    }
}
