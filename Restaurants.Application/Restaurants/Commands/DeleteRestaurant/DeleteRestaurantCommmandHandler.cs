﻿
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Definitions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommmandHandler : IRequestHandler<DeleteRestaurantCommand, RestaurantDefinition>
{
    private readonly ILogger<DeleteRestaurantCommmandHandler> _logger;
    private readonly IRestaurantsRepositoy _restaurantRepository;
    private readonly IMapper _mapper;

    public DeleteRestaurantCommmandHandler(
        ILogger<DeleteRestaurantCommmandHandler> logger,
        IRestaurantsRepositoy restaurantsRepositoy,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantRepository = restaurantsRepositoy;
    }

    public async Task<RestaurantDefinition> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting restaurant with id {RequestId}", request.Guid);
        var restaurant = await _restaurantRepository.Delete(request.Guid);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Guid.ToString());

        var restaurantDefinition = _mapper.Map<RestaurantDefinition>(restaurant);
        return restaurantDefinition;
    }
}
