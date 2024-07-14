
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand : IRequest<RestaurantDefinition>
{
    // General
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; } = false;

    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    // Address
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
}
