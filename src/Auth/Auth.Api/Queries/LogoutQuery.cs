namespace Auth.Api.Queries;

public record LogoutQuery(
    string RefreshToken,
    bool IsLogoutFromAllDevices);