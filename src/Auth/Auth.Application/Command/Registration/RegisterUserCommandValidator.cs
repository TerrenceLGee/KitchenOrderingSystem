using Auth.Application.Helpers;

using FluentValidation;

namespace Auth.Application.Command.Registration;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .MaximumLength(75)
            .WithMessage("{PropertyName} cannot exceed 75 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .MaximumLength(75)
            .WithMessage("{PropertyName} cannot exceed 75 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .MaximumLength(50)
            .WithMessage("{PropertyName} cannot exceed 50 characters.")
            .EmailAddress()
            .WithMessage("{PropertyName} is not a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .Must(ValidationHelpers.IsValidPassword)
            .WithMessage("{PropertyName} is not a valid password.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");

        RuleFor(x => x.ChallengeQuestion)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .MaximumLength(1024)
            .WithMessage("{PropertyName} cannot exceed 1024 characters.");

        RuleFor(x => x.ChallengeAnswer)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .MaximumLength(1024)
            .WithMessage("{PropertyName} cannot exceed 1024 characters.");
    }
}