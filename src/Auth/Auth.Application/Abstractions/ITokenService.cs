using Auth.Domain.Entities;

namespace Auth.Application.Abstractions;

public interface ITokenService
{
    Task<TokenResponse> GetAccessTokenAsync(
        User user, 
        UserRole[] userRoles, 
        CancellationToken cancellationToken);
}