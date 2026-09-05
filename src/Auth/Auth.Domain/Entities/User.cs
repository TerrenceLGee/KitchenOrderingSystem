namespace Auth.Domain.Entities.ValueObjects;

public class User
{
    public FirstName FirstName { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
}