using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<SigningKey> SigningKeys { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<UserPassword> UserPasswords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}