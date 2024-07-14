using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand : IRequest<RestaurantDefinition>
{
    public Guid Guid { get; set; }

    public DeleteRestaurantCommand(Guid guid)
    {
        Guid = guid;
    }
}
