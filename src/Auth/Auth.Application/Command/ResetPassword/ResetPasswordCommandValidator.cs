using Auth.Application.Helpers;

using FluentValidation;

namespace Auth.Application.Command.ResetPassword;

internal sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .EmailAddress()
            .WithMessage("{PropertyName} is invalid.");

        RuleFor(x => x.PreviousPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .Must(ValidationHelpers.IsValidPassword)
            .WithMessage("{PrpertyName} is invalid.");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} cannot be empty.")
            .NotNull()
            .WithMessage("{PropertyName} cannot be null.")
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match.");
    }
}