using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Persistance.Seeds.Seeders;
using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;

namespace Restaurants.Infrastructure.Persistance.Seeds;

public class DbInitializer(RestaurantsDbContext context, IEnumerable<IEntitySeeder> seeders) : IDbInitializer
{

    public async Task InitializeAsync()
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (await context.Database.CanConnectAsync())
        {
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
