using FluentValidation;

namespace Auth.Application.Command.Logout;

internal sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.");

        RuleFor(x => x.IsLogoutFromAllDevices)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.");
    }
}