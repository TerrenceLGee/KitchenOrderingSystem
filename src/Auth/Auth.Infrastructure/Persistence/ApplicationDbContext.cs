using Auth.Application.Abstractions;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence.Seeding;

using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<SigningKey> SigningKeys => Set<SigningKey>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<UserPassword> UserPasswords => Set<UserPassword>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var coreDataAdded = false;

                if (!await context.Set<Role>().AnyAsync(cancellationToken))
                {
                    var roles = Resources.GetRolesForSeeding();
                    await context.Set<Role>().AddRangeAsync(roles, cancellationToken);
                    coreDataAdded = true;
                }

                if (!await context.Set<User>().AnyAsync(cancellationToken))
                {
                    var users = Resources.GetUsersForSeeding();
                    await context.Set<User>().AddRangeAsync(users, cancellationToken);
                    coreDataAdded = true;
                }

                if (coreDataAdded)
                {
                    await context.SaveChangesAsync(cancellationToken);
                }

                var dependentDataAdded = false;

                if (!await context.Set<UserRole>().AnyAsync(cancellationToken))
                {
                    var userRoles = Resources.GetUserRolesForSeeding();
                    await context.Set<UserRole>().AddRangeAsync(userRoles, cancellationToken);
                    dependentDataAdded = true;
                }

                if (!await context.Set<UserPassword>().AnyAsync(cancellationToken))
                {
                    var userPasswords = Resources.GetUserPasswordsForSeeding();
                    await context.Set<UserPassword>().AddRangeAsync(userPasswords, cancellationToken);
                    dependentDataAdded = true;
                }

                if (dependentDataAdded)
                {
                    await context.SaveChangesAsync(cancellationToken);
                }
            })
            .UseSeeding((context, _) =>
            {
                var coreDataAdded = false;

                if (!context.Set<Role>().Any())
                {
                    var roles = Resources.GetRolesForSeeding();
                    context.Set<Role>().AddRange(roles);
                    coreDataAdded = true;
                }

                if (!context.Set<User>().Any())
                {
                    var users = Resources.GetUsersForSeeding();
                    context.Set<User>().AddRange(users);
                    coreDataAdded = true;
                }

                if (coreDataAdded)
                {
                    context.SaveChanges();
                }

                var dependentDataAdded = false;

                if (!context.Set<UserRole>().Any())
                {
                    var userRoles = Resources.GetUserRolesForSeeding();
                    context.Set<UserRole>().AddRange(userRoles);
                    dependentDataAdded = true;
                }

                if (!context.Set<UserPassword>().Any())
                {
                    var userPasswords = Resources.GetUserPasswordsForSeeding();
                    context.Set<UserPassword>().AddRange(userPasswords);
                    dependentDataAdded = true;
                }

                if (dependentDataAdded)
                {
                    context.SaveChangesAsync();
                }
            });
    }
}