using Restaurants.Application.Definitions;

namespace Restaurants.Application.Restaurants.Interfaces;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDefinition?>> GetAll();
    Task<RestaurantDefinition?> GetById(Guid restaurantGuid);
    Task<Guid> Create(CreateRestaurantDefinition restaurant);
}