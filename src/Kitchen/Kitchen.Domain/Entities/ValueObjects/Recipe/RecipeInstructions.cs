namespace Kitchen.Domain.Entities.ValueObjects.Recipe;

public sealed record RecipeInstructions
{
    public string Value { get; }

    public RecipeInstructions(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Recipe Instructions must have a value.");

        Value = value;
    }
}