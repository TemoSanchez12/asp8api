
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService : IRestaurantsService
{
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly ILogger<RestaurantsService> _logger;
    private readonly IMapper _mapper;

    public RestaurantsService(
        IRestaurantsRepositoy restaurantsRepository,
        ILogger<RestaurantsService> logger,
        IMapper mapper)
    {
        _restaurantRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDefinition?>> GetAll()
    {
        _logger.LogInformation("Fetching all restaurants");

        var restaurants = await _restaurantRepository.GetAllAsync();
        var restaurantsDefinition = _mapper.Map<IEnumerable<RestaurantDefinition>>(restaurants);

        return restaurantsDefinition;
    }

    public async Task<RestaurantDefinition?> GetById(Guid restaurantGuid)
    {
        _logger.LogInformation($"Fetching restaurant with guid {restaurantGuid}");

        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantGuid);
        var restaurantDefinition = _mapper.Map<RestaurantDefinition>(restaurant);

        return restaurantDefinition;
    }

    public async Task<Guid> Create(CreateRestaurantDefinition restaurant)
    {
        _logger.LogInformation($"Creating restaurant with name {restaurant.Name}");

        var restaurantEntity = _mapper.Map<Restaurant>(restaurant);

        var id = await _restaurantRepository.Create(restaurantEntity);

        return id;
    }
}
