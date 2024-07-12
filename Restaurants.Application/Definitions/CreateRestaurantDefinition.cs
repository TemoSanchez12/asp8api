namespace Restaurants.Application.Definitions;

public class CreateRestaurantDefinition
{
    // General
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; } = false;

    // Contact
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }

    // Address
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }


}
