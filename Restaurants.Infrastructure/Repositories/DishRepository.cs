
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository : IDishRepository
{
    private readonly RestaurantsDbContext _dbContext;

    public DishRepository(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dish> CreateDish(Dish dish)
    {
        await _dbContext.Dishes.AddAsync(dish);
        await _dbContext.SaveChangesAsync();
        return dish;
    }

    public async Task<IEnumerable<Dish>> GetAllForRestaurant(Guid guid)
    {
        var dishes = await _dbContext.Dishes.Where(dish => dish.RestaurantId == guid).ToListAsync();
        return dishes;
    }

    public async Task<Dish?> GetDishByIdAsync(int id)
    {
        var dish = await _dbContext.Dishes.FirstOrDefaultAsync(dish => dish.Id == id);
        return dish;
    }

    public async Task<Dish> RemoveDish(Dish dish)
    {
        _dbContext.Dishes.Remove(dish);
        await _dbContext.SaveChangesAsync();
        return dish;
    }
}
