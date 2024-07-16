
using AutoMapper;
using Restaurants.Application.Areas.Restaurants.Commands.CretateRestaurant;
using Restaurants.Application.Areas.Restaurants.Commands.UpdateRestaurant;
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

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, options => options.MapFrom(s => new Address
            {
                Street = s.Street,
                PostalCode = s.PostalCode,
                City = s.City,
            }));

        CreateMap<UpdateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Id, options => options.MapFrom(s => s.Guid))
            .ForMember(d => d.Address, options => options.MapFrom(s => new Address
            {
                Street = s.Street,
                PostalCode = s.PostalCode,
                City = s.City,
            }));
    }
}
