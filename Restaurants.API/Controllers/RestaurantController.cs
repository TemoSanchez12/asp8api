using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Definitions;
using Restaurants.Application.Restaurants.Interfaces;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantController : ControllerBase
{
    private IRestaurantsService _restaurantsService;

    public RestaurantController(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantsService.GetAll();
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}")]
    public async Task<IActionResult> GetById([FromRoute] string restaurantId)
    {
        var restaurantGuid = Guid.Parse(restaurantId);
        var restaurant = await _restaurantsService.GetById(restaurantGuid);

        if (restaurant == null)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDefinition restaurant)
    {
        var id = await _restaurantsService.Create(restaurant);
        return Ok(id);
    }

}
