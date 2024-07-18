
using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Areas.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private string[] allowedSortBy = [nameof(Restaurant.Description), nameof(Restaurant.Name), nameof(Restaurant.Category)];

    public GetAllRestaurantQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be equal or greater than 1");

        RuleFor(r => r.PageSize)
            .LessThanOrEqualTo(20)
            .WithMessage("Page size must be less or equal than 20");


    }
}
