using Auth.Application.Abstractions;
using Auth.Domain.Entities;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Command.Registration;

public class RegisterUserCommandHandler(
    IApplicationDbContext context,
    ILogger<RegisterUserCommandHandler> logger) : IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        var emailToCheck = command.Email.ToLower();
        var existingUser = await context.Users
            .FirstOrDefaultAsync(u => u.Email.Value.ToLower().Equals(emailToCheck), cancellationToken);

        if (existingUser is not null)
        {
            logger.LogWarning(
                "Someone tried to register a new account with email: ({Email}). " +
                "This email is already in use by another account",
                emailToCheck);
            
            return Result.Failure(new Error(
                "Email.AlreadyRegistered",
                "This email address is already in use.",
                ErrorType.Conflict));
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            hashedPassword,
            command.ChallengeQuestion,
            command.ChallengeQuestion);

        await context.Users.AddAsync(user, cancellationToken);

        var role = await context.Roles
            .FirstOrDefaultAsync(r => r.Name.Equals("Customer"), cancellationToken);

        if (role is not null)
        {
            var userRole = UserRole.Create(user.Id, role.Id);
            await context.UserRoles.AddAsync(userRole, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "A new user account for {FName} {LName} ({Email}) was " +
            "registered at {Time} utc time",
            command.FirstName,
            command.LastName,
            command.Email,
            DateTime.UtcNow);

        return Result.Success();
    }
}