
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CretateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
{
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateRestaurantCommandHandler(
        IRestaurantsRepositoy restaurantsRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper)
    {
        _restaurantRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating restaurant with name {@request}", request);

        var restaurantEntity = _mapper.Map<Restaurant>(request);

        var id = await _restaurantRepository.Create(restaurantEntity);

        return id;
    }
}
