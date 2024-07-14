
using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CretateRestaurant;

namespace Restaurants.Application.Definitions.Validators;

public class CreateRestaurantDefinitionValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];


    public CreateRestaurantDefinitionValidator()
    {
        RuleFor(res => res.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 100).WithMessage("Name must be between 3 and 100 characters");

        RuleFor(res => res.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(res => res.Category)
            .NotEmpty().WithMessage("Please enter a valid category")
            .Must(validCategories.Contains).WithMessage("Enter a valid category");

        RuleFor(res => res.ContactEmail)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address");
    }
}
