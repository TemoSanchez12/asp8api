
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Areas.Users.Commands.UnassignUserRole;

internal class UnassignUserRoleCommandHandler(
    ILogger<UnassignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
) : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassign user role: {@Request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail);
        var role = await roleManager.FindByNameAsync(request.RoleName);

        if (user == null)
            throw new NotFoundException(nameof(User), request.UserEmail);

        if (role == null)
            throw new NotFoundException(nameof(IdentityRole), request.RoleName);


        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
