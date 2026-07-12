using E_Commerce.Domain.Interfaces;

namespace E_Commerce.API.Extiontions
{
    public static class WebApplicationExtiontions
    {
        public static async Task<WebApplication>SeedDatabaseAsync(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetKeyedService<IDataSeeder>("Catalog");
            var Identityseeder = scope.ServiceProvider.GetKeyedService<IDataSeeder>("Identity");


            await seeder.DataSeedAsync();
            await Identityseeder.DataSeedAsync();

            return webApplication;
        }
    }
}
