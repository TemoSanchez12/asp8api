
using MediatR;
using Restaurants.Application.Definitions;

namespace Restaurants.Application.Areas.Dishes.Commands.CreateDish;

public class CreateDishCommand : IRequest<DishDefinition>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public Guid RestaurantId { get; set; }
}
