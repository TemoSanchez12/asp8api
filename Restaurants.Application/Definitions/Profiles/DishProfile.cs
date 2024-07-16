using AutoMapper;
using Restaurants.Application.Areas.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Definitions.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDefinition>();
        CreateMap<CreateDishCommand, Dish>();
    }
}
