using E_Commerce.Domain.Interfaces;

namespace E_Commerce.API.Extiontions
{
    public static class WebApplicationExtiontions
    {
        public static async Task<WebApplication>SeedDatabaseAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetKeyedService<IDataSeeder>("Catalog");

            await seeder.DataSeedAsync();

            return webApplication;
        }
    }
}
