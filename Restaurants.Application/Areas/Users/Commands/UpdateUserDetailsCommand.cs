
using MediatR;

namespace Restaurants.Application.Areas.Users.Commands;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly DateOfBirth { get; set; }
    public string Nationaltity { get; set; } = string.Empty;
}
