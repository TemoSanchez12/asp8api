
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<IEnumerable<Dish>> GetAllForRestaurant(Guid guid);
    Task<Dish> CreateDish(Dish dish);
    Task<Dish?> GetDishByIdAsync(int id);
    Task<Dish> RemoveDish(Dish dish);
}
