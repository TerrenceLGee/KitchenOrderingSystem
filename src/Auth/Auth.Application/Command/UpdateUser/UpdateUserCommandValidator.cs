using FluentValidation;

namespace Auth.Application.Command.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(75)
            .WithMessage("{PropertyName} cannot exceed 75 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(75)
            .WithMessage("{PropertyName} cannot exceed 75 characters.");

        RuleFor(x => x.Email)
            .MaximumLength(50)
            .WithMessage("{PropertyName} cannot exceed 50 characters.")
            .EmailAddress()
            .WithMessage("{PropertyName} is not a valid email address.");

        RuleFor(x => x.ChallengeQuestion)
            .MaximumLength(1024)
            .WithMessage("{PropertyName} cannot exceed 1024 characters.");

        RuleFor(x => x.ChallengeAnswer)
            .MaximumLength(1024)
            .WithMessage("{PropertyName} cannot exceed 1024 characters.");
    }
}