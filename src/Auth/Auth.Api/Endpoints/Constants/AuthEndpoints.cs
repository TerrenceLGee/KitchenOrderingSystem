using System.Security.Claims;

using Auth.Api.Extensions;
using Auth.Api.Queries;
using Auth.Application.Command.Login;
using Auth.Application.Command.Logout;
using Auth.Application.Command.Registration;
using Auth.Application.Command.ResetPassword;
using Auth.Application.Command.UpdateUser;

using MediatR;

namespace Auth.Api.Endpoints.Constants;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        var api = routes.MapGroup(AuthConstants.BaseUri)
            .WithTags(AuthConstants.Tag);

        api.MapPost(AuthConstants.Register, Register)
            .WithName("Register")
            .WithSummary("Register a new user account");

        api.MapPost(AuthConstants.Login, Login)
            .WithName("Login")
            .WithSummary("Login to a new user account");

        api.MapPost(AuthConstants.Logout, Logout)
            .WithName("Logout")
            .WithSummary("Logout of your user account")
            .RequireAuthorization();

        api.MapPut(AuthConstants.ResetPassword, ResetPassword)
            .WithName("ResetPassword")
            .WithSummary("Reset your old password to a new password");

        api.MapPut(AuthConstants.UpdateUserInfo, UpdateUserInfo)
            .WithName("UpdateInfo")
            .WithSummary("Update your information in the system");
    }

    private static async Task<IResult> Register(
        RegisterUserCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblemDetails();
    }

    private static async Task<IResult> Login(
        LoginCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : result.ToProblemDetails();
    }

    private static async Task<IResult> Logout(
        LogoutQuery query,
        HttpContext context,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var userIdClaim = context.User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
            return TypedResults.Unauthorized();

        var userEmailClaim = context.User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email);

        var email = userEmailClaim?.Value;

        var command = new LogoutCommand(
            userId,
            query.RefreshToken,
            query.IsLogoutFromAllDevices,
            email);

        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? TypedResults.NoContent()
            : result.ToProblemDetails();
    }

    private static async Task<IResult> ResetPassword(
        ResetPasswordCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblemDetails();
    }

    private static async Task<IResult> UpdateUserInfo(
        UpdateUserCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? TypedResults.NoContent()
            : result.ToProblemDetails();
    }
}