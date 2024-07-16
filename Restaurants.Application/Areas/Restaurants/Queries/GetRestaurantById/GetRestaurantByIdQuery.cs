
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Areas.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery : IRequest<RestaurantDefinition>
{
    public Guid Id { get; set; }

    public GetRestaurantByIdQuery(Guid guid)
    {
        Id = guid;
    }
}
