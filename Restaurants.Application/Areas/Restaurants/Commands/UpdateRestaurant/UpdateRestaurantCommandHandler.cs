
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, RestaurantDefinition>
{
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IRestaurantsRepositoy _restaurantRepository;

    public UpdateRestaurantCommandHandler(
        ILogger<UpdateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepositoy restaurantRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantRepository = restaurantRepository;
    }


    public async Task<RestaurantDefinition> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with id {RestaurantId} with {@UpdatedRestaurant}", request.Guid, request);
        var restaurant = _mapper.Map<Restaurant>(request);
        var restaurantUpdated = await _restaurantRepository.Update(restaurant);

        if (restaurantUpdated == null)
            throw new NotFoundException(nameof(Restaurant), request.Guid.ToString());

        var restaurantEntity = _mapper.Map<RestaurantDefinition>(restaurantUpdated);
        return restaurantEntity;
    }
}
