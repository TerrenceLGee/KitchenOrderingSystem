using Auth.Application.Abstractions;
using Auth.Domain.Entities;
using Auth.Domain.Entities.ValueObjects.User;

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
        var userEmail = new Email(command.Email.ToLower());

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken);

        if (user is null)
        {
            logger.LogWarning(
                "Someone with an invalid/non-existent account with email ({Email}) tried to reset their password",
                userEmail);
            return Result.Failure(new Error(
                "Invalid.Credentials",
                "Invalid credentials supplied",
                ErrorType.NotFound));
        }

        var isPreviousPasswordValid = BCrypt.Net.BCrypt.Verify(command.PreviousPassword, user.Password.Value);

        if (!isPreviousPasswordValid)
        {
            logger.LogWarning(
                "Someone using the email ({Email}) tried to reset the password associated with this account with an invalid 'previous' password.",
                userEmail);
            return Result.Failure(new Error(
                "PreviousPassword.Invalid",
                "Password cannot be reset because old password entered is invalid/incorrect",
                ErrorType.Unauthorized));
        }

        var isPreviouslyUsedPassword = false;

        var previousPasswords = await context.UserPasswords
            .Where(up => up.UserId == user.Id)
            .ToListAsync(cancellationToken);

        foreach (var password in previousPasswords)
        {
            isPreviouslyUsedPassword = BCrypt.Net.BCrypt.Verify(command.NewPassword, password.HashedPassword);
            if (isPreviouslyUsedPassword) break;
        }

        if (isPreviouslyUsedPassword)
        {
            logger.LogWarning(
                "User with email ({Email}) is trying to reset their password to a previously used password which is not allowed.",
                command.Email);
            return Result.Failure(new Error(
                "PreviousPassword.CannotBeReused",
                "Cannot reset password to a previously used password",
                ErrorType.BadRequest));
        }
        
        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
        user.ResetPassword(newHashedPassword);

        var newHashedPasswordToStore = UserPassword.Create(user.Id, newHashedPassword);

        await context.UserPasswords.AddAsync(newHashedPasswordToStore, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "User ({Email}) reset their password at {Time} utc time.",
            userEmail,
            DateTime.UtcNow);

        return Result.Success();
    }
}