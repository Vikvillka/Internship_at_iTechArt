using FluentValidation;

using CommunityHub.API.DTOs.EventDTOs;

namespace CommunityHub.API.Validators.Event;

public class UpdateEventRequestValidator : AbstractValidator<UpdateEventRequest> 
{
    public UpdateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(x => x.EventDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future");

        RuleFor(x => x.MaxParticipants)
            .GreaterThan(0).WithMessage("MaxParticipants must be greater than 0");

        RuleFor(x => x.Venue)
            .NotEmpty().WithMessage("Venue is required")
            .MaximumLength(200).WithMessage("Venue cannot exceed 200 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

        RuleFor(x => x.CommunityId)
            .NotEmpty().WithMessage("CommunityId is required");
    }
}

