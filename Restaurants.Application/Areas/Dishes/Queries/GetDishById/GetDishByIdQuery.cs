
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Areas.Dishes.Queries.GetDishById;

public class GetDishByIdQuery : IRequest<DishDefinition>
{
    public int Id { get; set; }
    public Guid RestaurantId { get; set; }

    public GetDishByIdQuery(int id, Guid restaurantId)
    {

        Id = id;
        RestaurantId = restaurantId;
    }
}
