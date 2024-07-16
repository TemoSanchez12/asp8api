using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Areas.Restaurants.Commands.CretateRestaurant;
using Restaurants.Application.Areas.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Areas.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Areas.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Areas.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Definitions;

namespace Restaurants.API.Controllers;

[ApiController]
[Authorize]
[Route("api/restaurants")]
public class RestaurantController : ControllerBase
{
    private IMediator _mediator;

    public RestaurantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDefinition>>> GetAll()
    {
        var restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}")]
    public async Task<ActionResult<RestaurantDefinition>> GetById([FromRoute] string restaurantId)
    {
        var restaurantGuid = Guid.Parse(restaurantId);
        var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(restaurantGuid));

        return Ok(restaurant);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete("{restaurantId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] string restaurantId)
    {
        var restaurant = await _mediator.Send(new DeleteRestaurantCommand(Guid.Parse(restaurantId)));
        return Ok(restaurant);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantCommand command)
    {
        var restaurant = await _mediator.Send(command);
        return Ok(restaurant);
    }
}
