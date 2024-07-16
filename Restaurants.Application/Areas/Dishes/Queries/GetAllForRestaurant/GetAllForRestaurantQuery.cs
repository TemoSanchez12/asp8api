
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Areas.Dishes.Queries.GetAllForRestaurant;

public class GetAllForRestaurantQuery : IRequest<IEnumerable<DishDefinition>>
{
    public Guid RestaurantId;
    public GetAllForRestaurantQuery(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }
}
