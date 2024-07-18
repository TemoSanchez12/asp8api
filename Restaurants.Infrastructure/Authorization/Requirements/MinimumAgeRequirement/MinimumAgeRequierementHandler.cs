
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Areas.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumAgeRequirement;

public class MinimumAgeRequierementHandler(
    ILogger<MinimumAgeRequierementHandler> logger,
    IUserContext userContext
) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurretUser();
        var dob = currentUser!.DateOfBirth;

        if (dob == null)
        {
            logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MInimumAgeRequirement", currentUser!.Email, dob);

        if (dob.Value.AddYears(requirement.Minimum) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
