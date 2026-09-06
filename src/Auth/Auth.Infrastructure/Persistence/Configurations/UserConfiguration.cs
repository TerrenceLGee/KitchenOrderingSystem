using Auth.Domain.Entities;
using Auth.Domain.Entities.ValueObjects.User;
using Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasConversion(new FirstNameConverter())
            .HasMaxLength(75)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasConversion(new LastNameConverter())
            .HasMaxLength(75)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasConversion(new EmailConverter())
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Password)
            .HasConversion(new PasswordConverter())
            .IsRequired();

        builder.ComplexProperty<Challenge>(
            u => u.Challenge,
            c =>
            {
                c.Property(ch => ch.Question)
                    .HasConversion(new ChallengeQuestionConverter())
                    .HasMaxLength(1024)
                    .IsRequired();

                c.Property(ch => ch.Answer)
                    .HasConversion(new ChallengeAnswerConverter())
                    .HasMaxLength(1024)
                    .IsRequired();
            });
    }
}