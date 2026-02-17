using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.ValueObjects;
using Restaurants.Infrastructure.Persistance.Seeds.Abstractions;
using Restaurants.Infrastructure.Seeds.Seeders.Helpers;

namespace Restaurants.Infrastructure.Persistance.Seeds.Seeders;

public class RestaurantSeeder(RestaurantsDbContext context) : IEntitySeeder
{

    public async Task SeedAsync()
    {
        if (await context.Restaurants.AnyAsync())
            return;
        var categories = await context.Categories
            .Select(c => new CategorySelector(c.Id, c.Name))
            .ToListAsync();
        await context.Restaurants.AddRangeAsync(GetRestaurants(categories));

        await context.SaveChangesAsync();
    }

    private IEnumerable<Restaurant> GetRestaurants(List<CategorySelector> categories)
    {
        var owner = new User
        {
            Email = "seed-user@test.com",
        };
        var fastFood = categories.Single(c => c.Name == "Fast Food");
        var italian = categories.Single(c => c.Name == "Italian");
        var japanese = categories.Single(c => c.Name == "Japanese");
        var chinese = categories.Single(c => c.Name == "Chinese");
        var indian = categories.Single(c => c.Name == "Indian");

        return new List<Restaurant>
        {
            new()
                    {
                        Owner = owner,
                        Name = "KFC",
                        CategoryId = fastFood.Id,
                        Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                        ContactEmail = "contact@kfc.com",
                        ContactNumber = "+44 20 1234 5678",
                        HasDelivery = true,
                        Address = new Address
                        {
                            City = "London",
                            Street = "Cork St 5",
                            PostalCode = "WC2N 5DU"
                        },
                        Dishes = new List<Dish>
                        {
                            new() { Name = "Zinger Burger", Description = "Spicy chicken fillet burger with lettuce and mayo.", Price = 6.99m, KiloCalories = 650 },
                            new() { Name = "Original Recipe Bucket", Description = "Classic fried chicken pieces seasoned with original recipe.", Price = 14.99m, KiloCalories = 1800 },
                            new() { Name = "Popcorn Chicken", Description = "Bite-sized crispy chicken pieces.", Price = 4.49m, KiloCalories = 420 },
                            new() { Name = "Fries (Regular)", Description = "Golden crispy fries.", Price = 2.29m, KiloCalories = 310 },
                        }
                    },
                    new()
                    {
                        Owner = owner,
                        Name = "McDonald's",
                        CategoryId = fastFood.Id,
                        Description = "McDonald's Corporation operates and franchises McDonald's restaurants worldwide.",
                        ContactEmail = "contact@mcdonalds.com",
                        ContactNumber = "+44 20 8765 4321",
                        HasDelivery = true,
                        Address = new Address
                        {
                            City = "London",
                            Street = "Boots 193",
                            PostalCode = "W1F 8SR"
                        },
                        Dishes = new List<Dish>
                        {
                            new() { Name = "Big Mac", Description = "Two beef patties, special sauce, lettuce, cheese, pickles, onions.", Price = 5.49m, KiloCalories = 550 },
                            new() { Name = "McChicken", Description = "Crispy chicken burger with lettuce and mayo.", Price = 4.99m, KiloCalories = 480 },
                            new() { Name = "Fries (Medium)", Description = "World-famous fries.", Price = 2.79m, KiloCalories = 340 },
                            new() { Name = "McFlurry Oreo", Description = "Soft serve ice cream with Oreo pieces.", Price = 3.49m, KiloCalories = 510 },
                        }
                    },
                    new()
                    {
                        Owner = owner,
                        Name = "Pizza Hut",
                        CategoryId = italian.Id,
                        Description = "Pizza Hut is an American multinational restaurant chain known for its Italian-American cuisine.",
                        ContactEmail = "contact@pizzahut.com",
                        ContactNumber = "+44 20 2345 6789",
                        HasDelivery = true,
                        Address = new Address
                        {
                            City = "Manchester",
                            Street = "Market St 45",
                            PostalCode = "M1 1WR"
                        },
                        Dishes = new List<Dish>
                        {
                            new() { Name = "Pepperoni Pizza (Medium)", Description = "Classic pepperoni with mozzarella and tomato sauce.", Price = 11.99m, KiloCalories = 1450 },
                            new() { Name = "Margherita Pizza (Medium)", Description = "Mozzarella and tomato sauce with basil.", Price = 9.99m, KiloCalories = 1250 },
                            new() { Name = "Garlic Bread", Description = "Toasted garlic bread slices.", Price = 4.29m, KiloCalories = 620 },
                            new() { Name = "Chicken Wings (6 pcs)", Description = "Oven-baked wings with your choice of sauce.", Price = 6.99m, KiloCalories = 780 },
                        }
                    }

        };
    }
}