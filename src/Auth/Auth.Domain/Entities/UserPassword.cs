namespace Auth.Domain.Entities;

public class UserPassword : BaseEntity
{
    public Guid UserId { get; private set; }
    public string HashedPassword { get; private set; } = null!;
    
    private UserPassword() {}

    private UserPassword(Guid userId, string hashedPassword)
    {
        UserId = userId;
        HashedPassword = hashedPassword;
    }

    public static UserPassword Create(Guid userId, string hashedPassword)
    {
        return new UserPassword(userId, hashedPassword);
    }
}