namespace Kitchen.Domain.Entities.ValueObjects.Recipe;

public sealed record RecipeName
{
    public string Value { get; }

    public RecipeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Recipe name must have a value.");

        if (value.Length > 150)
            throw new ArgumentException("Recipe name cannot exceed 150 characters.");

        Value = value;
    }
}