using Kitchen.Domain.Entities;
using Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.RecipeIngredient;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitchen.Infrastructure.Persistence.Configurations;

public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.ToTable("recipe_ingredients");

        builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        builder.HasOne(ri => ri.Recipe)
            .WithMany(r => r.Ingredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .HasForeignKey(ri => ri.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ri => ri.Quantity)
            .IsRequired()
            .HasConversion(new IngredientQuantityConverter());

        builder.Property(ri => ri.UnitOfMeasurement)
            .IsRequired();
    }
}