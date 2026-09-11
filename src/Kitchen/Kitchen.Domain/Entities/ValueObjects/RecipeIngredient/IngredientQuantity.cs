namespace Kitchen.Domain.Entities.ValueObjects.RecipeIngredient;

public sealed record IngredientQuantity
{
    public decimal Value { get; }

    public IngredientQuantity(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Recipe ingredient cannot be less than equal to 0");
        
        Value = value;
    }
}