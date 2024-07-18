
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommmandHandler : IRequestHandler<DeleteRestaurantCommand, RestaurantDefinition>
{
    private readonly ILogger<DeleteRestaurantCommmandHandler> _logger;
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IRestaurantAuthorizationService _restaurantAuthorizationService;

    public DeleteRestaurantCommmandHandler(
        IRestaurantAuthorizationService restaurantAuthorizationService,
        ILogger<DeleteRestaurantCommmandHandler> logger,
        IRestaurantsRepositoy restaurantsRepositoy,
        IMapper mapper)
    {
        _restaurantAuthorizationService = restaurantAuthorizationService;
        _logger = logger;
        _mapper = mapper;
        _restaurantRepository = restaurantsRepositoy;
    }

    public async Task<RestaurantDefinition> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting restaurant with id {RequestId}", request.Guid);
        var restaurantEntity = await _restaurantRepository.GetByIdAsync(request.Guid);

        if (restaurantEntity == null)
            throw new NotFoundException(nameof(Restaurant), request.Guid.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurantEntity, ResourceOperation.Delete))
            throw new ForbidException();

        var restaurant = await _restaurantRepository.Delete(request.Guid);

        var restaurantDefinition = _mapper.Map<RestaurantDefinition>(restaurant);
        return restaurantDefinition;
    }
}
