using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Definitions.Profiles;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDefinition>();
    }
}
