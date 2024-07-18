
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Areas.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Areas.Restaurants.Commands.CretateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
{
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateRestaurantCommandHandler(
        IRestaurantsRepositoy restaurantsRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IUserContext userContext)
    {
        _restaurantRepository = restaurantsRepository;
        _logger = logger;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurretUser();
        _logger.LogInformation("{UserName} [{UserId}] is creating restaurant with name {@request}", currentUser!.Email, currentUser.Id, request);

        var restaurantEntity = _mapper.Map<Restaurant>(request);
        restaurantEntity.OwnerId = currentUser.Id;

        var id = await _restaurantRepository.Create(restaurantEntity);

        return id;
    }
}
