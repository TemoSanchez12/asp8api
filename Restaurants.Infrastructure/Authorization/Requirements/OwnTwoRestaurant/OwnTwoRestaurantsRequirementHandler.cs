
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Areas.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements.OwnTwoRestaurant;

public class OwnTwoRestaurantsRequirementHandler(
    ILogger<OwnTwoRestaurantsRequirementHandler> logger,
    IUserContext userContext,
    IRestaurantsRepositoy restaurantRepository
) : AuthorizationHandler<OwnTwoRestaurantRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnTwoRestaurantRequirement requirement)
    {
        var currentUser = userContext.GetCurretUser();
        var restaurants = await restaurantRepository.GetRestaurantByOwner(currentUser!.Id);

        if (restaurants.Count() >= requirement.Minimum)
        {
            logger.LogInformation("User has more or 2 restaurants");
            context.Succeed(requirement);
            return;
        }

        context.Fail();
        return;
    }
}
