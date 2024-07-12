
using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Definitions.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDefinition>()
            .ForMember(d => d.City, options => options.MapFrom(s => s.Address != null ? s.Address.City : null))
            .ForMember(d => d.Street, options => options.MapFrom(s => s.Address != null ? s.Address.Street : null))
            .ForMember(d => d.PostalCode, options => options.MapFrom(s => s.Address != null ? s.Address.PostalCode : null))
            .ForMember(d => d.Dishes, options => options.MapFrom(s => s.Dishes));

        CreateMap<CreateRestaurantDefinition, Restaurant>()
            .ForMember(d => d.Address, options => options.MapFrom(s => new Address
            {
                Street = s.Street,
                PostalCode = s.PostalCode,
                City = s.City,
            }));
    }
}
