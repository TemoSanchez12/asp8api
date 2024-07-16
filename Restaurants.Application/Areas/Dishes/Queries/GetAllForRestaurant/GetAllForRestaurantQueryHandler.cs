
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Dishes.Queries.GetAllForRestaurant;

public class GetAllForRestaurantQueryHandler : IRequestHandler<GetAllForRestaurantQuery, IEnumerable<DishDefinition>>
{
    private readonly IDishRepository _dishRepository;
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private ILogger<GetAllForRestaurantQueryHandler> _logger;
    private IMapper _mapper;

    public GetAllForRestaurantQueryHandler(
        IDishRepository dishRepository,
        IRestaurantsRepositoy restaurantsRepositoy,
        ILogger<GetAllForRestaurantQueryHandler> logger,
        IMapper mapper)
    {
        _dishRepository = dishRepository;
        _logger = logger;
        _mapper = mapper;
        _restaurantRepository = restaurantsRepositoy;
    }


    public async Task<IEnumerable<DishDefinition>> Handle(GetAllForRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching dishes for restaurant with id: {RestaurandId}", request.RestaurantId);

        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dishesEntities = await _dishRepository.GetAllForRestaurant(request.RestaurantId);
        var dishesDefinitions = dishesEntities.Select(_mapper.Map<DishDefinition>).ToList();
        return dishesDefinitions;
    }
}
