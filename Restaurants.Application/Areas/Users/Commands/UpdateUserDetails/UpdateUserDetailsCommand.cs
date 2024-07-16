
using MediatR;

namespace Restaurants.Application.Areas.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly DateOfBirth { get; set; }
    public string Nationaltity { get; set; } = string.Empty;
}
