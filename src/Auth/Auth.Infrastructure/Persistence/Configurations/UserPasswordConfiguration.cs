using Auth.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations;

public class UserPasswordConfiguration : IEntityTypeConfiguration<UserPassword>
{
    public void Configure(EntityTypeBuilder<UserPassword> builder)
    {
        builder.ToTable("users_previous_passwords");

        builder.HasKey(up => up.Id);

        builder.Property(up => up.HashedPassword)
            .IsRequired();
    }
}