using FluentValidation;

using CommunityHub.Contracts.DTOs.EventDTOs;

namespace CommunityHub.API.Validators.Event;

public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(x => x.EventDate)
            .Must(date => date > DateTime.UtcNow).WithMessage("Event date must be in the future");

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

        RuleForEach(x => x.TagIds)
            .NotEmpty().WithMessage("TagId cannot be empty");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be a valid EventStatus value");
    }
}

