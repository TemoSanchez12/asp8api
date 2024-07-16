using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Dishes.Commands.DeleteDish;

public class DeleteDishCommandHandler(
    ILogger<DeleteDishCommandHandler> logger,
    IMapper mapper,
    IDishRepository dishRepository,
    IRestaurantsRepositoy restaurantsRepositoy
) : IRequestHandler<DeleteDishCommand, DishDefinition>
{
    public async Task<DishDefinition> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with id: {DishId}, for restaurant: {RestaurantId}", request.Id, request.RestaurantId);

        var restaurant = await restaurantsRepositoy.GetByIdAsync(request.RestaurantId);
        var dish = await dishRepository.GetDishByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (dish == null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());


        var dishEntity = await dishRepository.RemoveDish(dish);
        var dishDefinition = mapper.Map<DishDefinition>(dishEntity);

        return dishDefinition;
    }
}
