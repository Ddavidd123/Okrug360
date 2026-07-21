using FluentValidation;
using Okrug360.Places.Api.Dtos;

namespace Okrug360.Places.Api.Validators;

public sealed class UpdatePlaceRequestValidator : AbstractValidator<UpdatePlaceRequest>
{
    public UpdatePlaceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(4000);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(300);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Category is invalid.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180);

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));
    }
}