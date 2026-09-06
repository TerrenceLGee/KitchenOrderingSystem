using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence.Configurations.ValueConverters.SigningKey;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations;

public class SigningKeyConfiguration : IEntityTypeConfiguration<SigningKey>
{
    public void Configure(EntityTypeBuilder<SigningKey> builder)
    {
        builder.ToTable("signing_keys");

        builder.HasKey(sk => sk.Id);

        builder.Property(sk => sk.KeyId)
            .IsRequired();

        builder.Property(sk => sk.PrivateKey)
            .HasConversion(new PrivateKeyConverter())
            .IsRequired();

        builder.Property(sk => sk.PublicKey)
            .HasConversion(new PublicKeyConverter())
            .IsRequired();

        builder.Property(sk => sk.IsActive)
            .IsRequired();

        builder.Property(sk => sk.ExpiresAtUtc)
            .IsRequired();

        builder.HasIndex(sk => sk.IsActive);
    }
}