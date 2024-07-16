
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Areas.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDefinition>
{
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetRestaurantByIdQueryHandler(
        IRestaurantsRepositoy restaurantsRepository,
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper)
    {
        _restaurantRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RestaurantDefinition> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching restaurant with id {RequestId}", request.Id);

        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantDefinition = _mapper.Map<RestaurantDefinition>(restaurant);

        return restaurantDefinition;
    }
}
