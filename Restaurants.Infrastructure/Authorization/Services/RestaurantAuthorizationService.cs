
using Microsoft.Extensions.Logging;
using Restaurants.Application.Areas.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(
    ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext
) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var user = userContext.GetCurretUser();
        logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {Restaurant}", user!.Email, operation, restaurant.Name);

        if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/read operation - successful authorization");
            return true;
        }

        if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((operation == ResourceOperation.Delete || operation == ResourceOperation.Update) && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }

        logger.LogWarning("User not have the necesary credentials");
        return false;

    }
}
