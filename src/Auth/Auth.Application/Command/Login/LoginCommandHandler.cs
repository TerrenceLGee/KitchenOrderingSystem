using Auth.Application.Abstractions;
using Auth.Domain.Entities.ValueObjects.User;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Command.Login;

public class LoginCommandHandler(
    IApplicationDbContext context,
    ITokenService tokenService,
    ILogger<LoginCommandHandler> logger) : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken)
    {
        var userEmail = new Email(command.Email.ToLower());

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail, cancellationToken);

        if (user is null)
        {
            logger.LogWarning(
                "Someone tried to login to the system with an invalid email address ({Email})",
                userEmail);
            return Result.Failure<TokenResponse>(new Error(
                "Invalid.Credentials",
                "Invalid credentials provided.",
                ErrorType.NotFound));
        }

        var userRoles = await context.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .ToArrayAsync(cancellationToken);

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(command.Password, user.Password.Value);

        if (!isPasswordValid)
        {
            logger.LogWarning(
                "A login with email ({Email}) was attempted with an invalid/incorrect password.",
                userEmail);
            return Result.Failure<TokenResponse>(new Error(
                "Invalid Credentials",
                "Invalid credentials provided.",
                ErrorType.Unauthorized));
        }

        var response = await tokenService.GetAccessTokenAsync(user, userRoles, cancellationToken);
        
        logger.LogInformation(
            "User ({Email}) logged into the system at: {TimeUtc} utc time.",
            userEmail,
            DateTime.UtcNow);

        return Result.Success(response);
    }
}