using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder : IRestaurantSeeder
{
    private RestaurantsDbContext _context;

    public RestaurantSeeder(RestaurantsDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Seed()
    {
        if (!_context.Restaurants.Any())
        {
            var restaurants = GetRestaurants();
            _context.Restaurants.AddRange(restaurants);
            await _context.SaveChangesAsync();
        }

        if (!_context.Roles.Any())
        {
            var roles = GetRoles();
            _context.Roles.AddRange(roles);
            await _context.SaveChangesAsync();
        }

    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        var roles = new List<IdentityRole>()
        {
            new (UserRoles.User) { NormalizedName = UserRoles.User.ToUpper() },
            new (UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpper() },
            new (UserRoles.Owner) { NormalizedName = UserRoles.Owner.ToUpper() }
        };

        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {

        var guidBellaItalia = Guid.NewGuid();
        var guidShushiWorld = Guid.NewGuid();
        var guidBurgerHaven = Guid.NewGuid();

        var restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Id = guidBellaItalia,
                Name = "La Bella Italia",
                Description = "Authentic Italian cuisine with a cozy atmosphere.",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "info@labellaitalia.com",
                ContactNumber = "555-1234",
                Address = new Address
                {
                    City = "Rome",
                    Street = "Via Roma 10",
                    PostalCode = "00100"
                },
                Dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name = "Spaghetti Carbonara",
                        Description = "Classic Roman pasta dish with eggs, cheese, pancetta, and pepper.",
                        Price = 12.50m,
                        RestaurantId = guidBellaItalia
                    },
                    new Dish
                    {
                        Name = "Margherita Pizza",
                        Description = "Traditional pizza with tomatoes, mozzarella cheese, fresh basil, salt, and olive oil.",
                        Price = 8.99m,
                        RestaurantId = guidBellaItalia
                    }
                }
            },
            new Restaurant
            {
                Id = guidShushiWorld,
                Name = "Sushi World",
                Description = "Fresh and delicious sushi made to order.",
                Category = "Japanese",
                HasDelivery = true,
                ContactEmail = "contact@sushiworld.com",
                ContactNumber = "555-5678",
                Address = new Address
                {
                    City = "Tokyo",
                    Street = "Shibuya 3-22-1",
                    PostalCode = "150-0002"
                },
                Dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name = "California Roll",
                        Description = "Sushi roll with crab, avocado, and cucumber.",
                        Price = 6.50m,
                        RestaurantId = guidShushiWorld
                    },
                    new Dish
                    {
                        Name = "Tuna Sashimi",
                        Description = "Fresh tuna slices served with soy sauce and wasabi.",
                        Price = 10.00m,
                        RestaurantId = guidShushiWorld
                    }
                }
            },
            new Restaurant
            {
                Id = guidBurgerHaven,
                Name = "Burger Haven",
                Description = "The best burgers in town with a variety of toppings.",
                Category = "American",
                HasDelivery = false,
                ContactEmail = "hello@burgerhaven.com",
                ContactNumber = "555-8765",
                Address = new Address
                {
                    City = "New York",
                    Street = "5th Avenue 101",
                    PostalCode = "10001"
                },
                Dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name = "Classic Cheeseburger",
                        Description = "Juicy beef patty with cheddar cheese, lettuce, tomato, and pickles.",
                        Price = 9.99m,
                        RestaurantId = guidBurgerHaven
                    },
                    new Dish
                    {
                        Name = "Bacon Avocado Burger",
                        Description = "Burger with crispy bacon, avocado slices, and pepper jack cheese.",
                        Price = 11.50m,
                        RestaurantId = guidBurgerHaven
                    }
                }
            }
        };


        return restaurants;
    }
}

