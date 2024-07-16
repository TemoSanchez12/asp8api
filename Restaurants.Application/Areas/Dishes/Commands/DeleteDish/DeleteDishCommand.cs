using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Areas.Dishes.Commands.DeleteDish;

public class DeleteDishCommand : IRequest<DishDefinition>
{
    public int Id { get; set; }
    public Guid RestaurantId { get; set; }

    public DeleteDishCommand(int id, Guid restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
