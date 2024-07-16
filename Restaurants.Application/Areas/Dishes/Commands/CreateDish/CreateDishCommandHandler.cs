using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, DishDefinition>
{
    private readonly IDishRepository _dishRepository;
    private readonly IRestaurantsRepositoy _restaurantsRepositoy;
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateDishCommandHandler(
        IRestaurantsRepositoy restaurantsRepositoy,
        IDishRepository dishRepository,
        ILogger<CreateDishCommandHandler> logger,
        IMapper mapper)
    {
        _restaurantsRepositoy = restaurantsRepositoy;
        _dishRepository = dishRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<DishDefinition> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating dish {@request}", request);

        var restaurant = await _restaurantsRepositoy.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());



        var dishEntity = _mapper.Map<Dish>(request);
        var dishDefinition = _mapper.Map<DishDefinition>(await _dishRepository.CreateDish(dishEntity));

        return dishDefinition;
    }
}
