
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDefinition>>
{
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;


    public GetAllRestaurantsQueryHandler(
        IRestaurantsRepositoy restaurantsRepository,
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper)
    {
        _restaurantRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PageResult<RestaurantDefinition>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all restaurants");

        var (restaurants, totalCount) = await _restaurantRepository.GetAllMatchingAsync(
            request.SearchPhrase,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection
            );

        var restaurantsDefinition = _mapper.Map<IEnumerable<RestaurantDefinition>>(restaurants);

        var result = new PageResult<RestaurantDefinition>(restaurantsDefinition, totalCount, request.PageSize, request.PageNumber);

        return result;
    }
}
