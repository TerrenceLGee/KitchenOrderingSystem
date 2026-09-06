using FluentValidation;

namespace Auth.Application.Command.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .EmailAddress()
            .WithMessage("{PropertyName} is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.");
    }
}