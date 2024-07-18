﻿
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAgeRequirement;
public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int Minimum { get; } = minimumAge;
}
