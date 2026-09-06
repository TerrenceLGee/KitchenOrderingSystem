using Auth.Application.Abstractions;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Command.UpdateUser;

public class UpdateUserCommandHandler(
    IApplicationDbContext context,
    ILogger<UpdateUserCommandHandler> logger) : IRequestHandler<UpdateUserCommand, Result>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            logger.LogWarning(
                "There is no user found with Id {UserId} in the system, someone is trying to update a non-existent user account",
                command.UserId);
            return Result.Failure(new Error(
                "User.NotFound",
                "There is no user found with that Id",
                ErrorType.NotFound));
        }
        
        if (!string.IsNullOrEmpty(command.FirstName)) user.UpdateFirstName(command.FirstName);
        
        if (!string.IsNullOrEmpty(command.LastName)) user.UpdateLastName(command.LastName);
        
        if (!string.IsNullOrEmpty(command.Email)) user.UpdateEmailAddress(command.Email);
        
        if (!string.IsNullOrEmpty(command.ChallengeQuestion) && !string.IsNullOrEmpty(command.ChallengeAnswer))
            user.UpdateChallenge(command.ChallengeQuestion, command.ChallengeAnswer);

        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "User with Id {UserId} updated their account information at {Time} utc time.",
            command.UserId,
            DateTime.UtcNow);

        return Result.Success();
    }
}