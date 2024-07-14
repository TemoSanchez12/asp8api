
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDefinition>>
{
}
