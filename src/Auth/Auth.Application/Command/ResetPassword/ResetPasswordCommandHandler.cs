using Auth.Application.Abstractions;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Command.ResetPassword;

public class ResetPasswordCommandHandler(
    IApplicationDbContext context,
    ILogger<ResetPasswordCommandHandler> logger) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(
        ResetPasswordCommand command, 
        CancellationToken cancellationToken)
    {
        var userEmail = command.Email.ToLower();

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email.Value.ToLower().Equals(userEmail), cancellationToken);

        if (user is null)
        {
            logger.LogWarning(
                "Someone with an invalid/non-existent account with email ({Email}) tried to reset their password",
                userEmail);
            return Result.Failure(new Error(
                "Invalid.Credentials",
                "Invalid credentials supplied",
                ErrorType.Unauthorized));
        }

        var isOldPasswordValid = BCrypt.Net.BCrypt.Verify(command.OldPassword, user.Password.Value);

        if (!isOldPasswordValid)
        {
            logger.LogWarning(
                "Someone using the email ({Email}) tried to reset the password associated with this account with an invalid 'old' password.",
                userEmail);
            return Result.Failure(new Error(
                "OldPassword.Invalid",
                "Password cannot be reset because old password entered is invalid/incorrect",
                ErrorType.Unauthorized));
        }

        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
        
        user.ResetPassword(newHashedPassword);

        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "User ({Email}) reset their password at {Time} utc time.",
            userEmail,
            DateTime.UtcNow);

        return Result.Success();
    }
}