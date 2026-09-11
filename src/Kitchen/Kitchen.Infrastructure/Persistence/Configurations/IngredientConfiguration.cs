using Kitchen.Domain.Entities;
using Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Ingredient;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kitchen.Infrastructure.Persistence.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("ingredients");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(120)
            .HasConversion(new IngredientNameConverter());

        builder.Property(i => i.Category)
            .IsRequired();

        builder.Property(i => i.IsInStock)
            .IsRequired();

        builder.Property(i => i.QuantityInStock)
            .IsRequired()
            .HasConversion(new StockConverter());
    }
}