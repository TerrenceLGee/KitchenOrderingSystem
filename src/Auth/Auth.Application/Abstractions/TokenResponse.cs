namespace Auth.Application.Abstractions;

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpirationUtc,
    DateTime RefreshTokenExpirationUtc);