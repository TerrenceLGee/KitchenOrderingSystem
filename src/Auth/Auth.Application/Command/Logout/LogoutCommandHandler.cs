using System.Security.Cryptography;
using System.Text;

using Auth.Application.Abstractions;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Command.Logout;

public class LogoutCommandHandler(
    IApplicationDbContext context,
    ILogger<LogoutCommandHandler> logger) : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(
        LogoutCommand command, 
        CancellationToken cancellationToken)
    {
        var hashedToken = HashToken(command.RefreshToken);

        var storedRefreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token.Value == hashedToken
                                       && rt.UserId == command.UserId, cancellationToken);

        if (storedRefreshToken is null)
        {
            logger.LogWarning(
                "User ({Info}) is trying to logout with an invalid/non-existent refresh token.",
                command.Email ?? command.UserId.ToString());
            return Result.Failure(new Error(
                "RefreshToken.Invalid",
                "Invalid Refresh Token",
                ErrorType.Unauthorized));
        }

        if (storedRefreshToken.IsRevoked)
        {
            logger.LogWarning(
                "User ({Info}) is trying to logout with an already revoked refresh token.",
                command.Email ?? command.UserId.ToString());
            return Result.Failure(new Error(
                "RefreshToken.AlreadyRevoked",
                "Refresh token is already revoked",
                ErrorType.Unauthorized));
        }
        
        storedRefreshToken.Revoke();

        if (command.IsLogoutFromAllDevices)
        {
            var userRefreshTokens = await context.RefreshTokens
                .Where(rt => rt.UserId == storedRefreshToken.UserId && !rt.IsRevoked)
                .ToListAsync(cancellationToken);

            foreach (var token in userRefreshTokens)
            {
                token.Revoke();
            }
        }

        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "User ({Info}) logged out of the system at {Time} utc time",
            command.Email ?? command.UserId.ToString(),
            DateTime.UtcNow);
        return Result.Success();
    }

    private static string HashToken(string token)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashedBytes);
    }
}