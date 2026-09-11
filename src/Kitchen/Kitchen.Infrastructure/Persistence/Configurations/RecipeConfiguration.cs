using Kitchen.Domain.Entities;
using Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Recipe;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitchen.Infrastructure.Persistence.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("recipes");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasConversion(new RecipeNameConverter());

        builder.Property(r => r.Instructions)
            .IsRequired()
            .HasConversion(new RecipeInstructionsConverter());

        builder.Property(r => r.Category)
            .IsRequired();
    }
}