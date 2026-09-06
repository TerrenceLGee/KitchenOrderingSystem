using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence.Configurations.ValueConverters.Role;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasConversion(new RoleNameConverter())
            .IsRequired();

        builder.Property(r => r.Description)
            .HasConversion(new RoleDescriptionConverter())
            .IsRequired();

        builder.HasIndex(r => r.Name.Value);
    }
}