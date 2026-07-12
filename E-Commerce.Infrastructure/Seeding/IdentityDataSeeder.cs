using E_Commerce.Domain.Interfaces;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Seeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext , UserManager<ApplicationUser> userManager 
            ,RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task DataSeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);

                if (pendingMigrations.Any()) await _dbContext.Database.MigrateAsync(ct);

                if (!await _roleManager.Roles.AnyAsync(ct))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await _userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser
                    {
                        DisplayName = "Mohamed Ahmed",
                        Email = "mohamedahmed@gmail.com",
                        UserName = "mohamed",
                        PhoneNumber = "01232112332"
                    };
                    var createResult = await _userManager.CreateAsync(admin, "P@ssw0rd");
                    if (createResult.Succeeded) await _userManager.AddToRoleAsync(admin, "Admin");

                    else
                    {
                        _logger.LogWarning("Coudn't seed the default users:{Errors}", string.Join(";", createResult.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Identity data seeding failed");                       
            }
        }
    }
}
