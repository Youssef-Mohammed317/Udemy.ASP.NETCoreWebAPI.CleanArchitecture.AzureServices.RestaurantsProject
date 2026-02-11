using Restaurants.Infrastructure.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants.Infrastructure.Persistance.Seeds.Abstractions;

public interface IEntitySeeder
{
    Task SeedAsync();
}
