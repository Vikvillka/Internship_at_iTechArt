using FluentValidation;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;

namespace CommunityHub.API.Validators.Community;

public class CreateCommunityRequestValidator : AbstractValidator<CreateCommunityRequest>
{
    public CreateCommunityRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1500).WithMessage("Description cannot exceed 1500 characters");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(50).WithMessage("City cannot exceed 50 characters");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(50).WithMessage("Country cannot exceed 50 characters");
    }
}

