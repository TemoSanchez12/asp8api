
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler(
    ILogger<GetDishByIdQueryHandler> logger,
    IMapper mapper,
    IDishRepository dishRepository,
    IRestaurantsRepositoy restaurantsRepositoy
) : IRequestHandler<GetDishByIdQuery, DishDefinition>
{
    public async Task<DishDefinition> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting dish with id: {DishId}", request.Id);
        var dishEntity = await dishRepository.GetDishByIdAsync(request.Id);
        var restaurant = await restaurantsRepositoy.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (dishEntity == null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());

        var dishDefinition = mapper.Map<DishDefinition>(dishEntity);
        return dishDefinition;
    }
}
