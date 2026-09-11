namespace Kitchen.Domain.Entities.ValueObjects.Ingredient;

public sealed record IngredientName
{
    public string Value { get; }

    public IngredientName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Ingredient name must have a value.");
        
        if (value.Length > 120)
            throw new ArgumentException("Ingredient name cannot exceed 120 characters.");
        
        Value = value;
    }
}