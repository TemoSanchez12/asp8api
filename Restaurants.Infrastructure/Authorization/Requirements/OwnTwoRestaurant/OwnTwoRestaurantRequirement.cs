using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.OwnTwoRestaurant;

public class OwnTwoRestaurantRequirement(int minimum) : IAuthorizationRequirement
{
    public int Minimum { get; } = minimum;
}
