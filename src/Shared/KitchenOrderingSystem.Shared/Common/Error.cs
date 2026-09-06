namespace KitchenOrderingSystem.Shared.Common;

public record Error(
    string Code, 
    string Description, 
    ErrorType ErrorType)
{
    public static readonly Error None = new(
        string.Empty,
        string.Empty,
        ErrorType.None);

    public static readonly Error NullValue = new(
        "Error.NullValue",
        "Null value was provided",
        ErrorType.NullValue);
}