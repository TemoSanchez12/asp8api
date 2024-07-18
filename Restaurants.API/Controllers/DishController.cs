using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Areas.Dishes.Commands.CreateDish;
using Restaurants.Application.Areas.Dishes.Commands.DeleteDish;
using Restaurants.Application.Areas.Dishes.Queries.GetAllForRestaurant;
using Restaurants.Application.Areas.Dishes.Queries.GetDishById;
using Restaurants.Application.Definitions;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{RestaurantId}/dishes")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AtLeast20)]
    public async Task<ActionResult<IEnumerable<DishDefinition>>> GetAllForRestaurant([FromRoute] string restaurantId)
    {
        var dishes = await _mediator.Send(new GetAllForRestaurantQuery(Guid.Parse(restaurantId)));
        return Ok(dishes.ToList());
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDefinition>> GetDishById([FromRoute] string dishId, [FromRoute] string restaurantId)
    {

        var dish = await _mediator.Send(new GetDishByIdQuery(int.Parse(dishId), Guid.Parse(restaurantId)));
        return Ok(dish);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] string restaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = Guid.Parse(restaurantId);
        var dishCreated = await _mediator.Send(command);
        return Ok(dishCreated);
    }

    [HttpDelete("{dishId}")]
    public async Task<ActionResult<DishDefinition>> RemoveDish([FromRoute] string dishId, [FromRoute] string restaurantId)
    {
        Console.WriteLine("Entro a la ruta");
        var dish = await _mediator.Send((new DeleteDishCommand(int.Parse(dishId), Guid.Parse(restaurantId))));
        return Ok(dish);
    }
}
