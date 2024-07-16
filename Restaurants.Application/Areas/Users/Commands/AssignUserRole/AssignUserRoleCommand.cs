
using MediatR;

namespace Restaurants.Application.Areas.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
