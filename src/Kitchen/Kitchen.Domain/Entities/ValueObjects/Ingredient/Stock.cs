using Kitchen.Domain.Entities.Enums.RecipeIngredient;
using Kitchen.Domain.Entities.ValueObjects.RecipeIngredient;

namespace Kitchen.Domain.Entities.ValueObjects.Ingredient;

public sealed record Stock
{
    public decimal Quantity { get; }
    public UnitOfMeasurement UnitOfMeasurement { get; }

    public Stock(decimal quantity, UnitOfMeasurement unitOfMeasurement)
    {
        if (quantity < 0)
            throw new ArgumentException("Ingredient stock cannot be a negative value less than zero.");

        Quantity = quantity;
        UnitOfMeasurement = unitOfMeasurement;
    }
}