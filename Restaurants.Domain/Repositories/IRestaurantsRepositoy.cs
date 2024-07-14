
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepositoy
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant?> GetByIdAsync(Guid restaurantGuid);
    Task<Guid> Create(Restaurant restaurant);
    Task<Restaurant?> Delete(Guid restaurantGuid);
    Task<Restaurant?> Update(Restaurant restaurant);
}
