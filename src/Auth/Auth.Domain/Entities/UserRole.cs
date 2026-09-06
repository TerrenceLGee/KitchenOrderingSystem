namespace Auth.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; private set; }
    public User User { get; set; } = null!;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    
    private UserRole() {}

    private UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public static UserRole Create(Guid userId, Guid roleId)
    {
        return new UserRole(userId, roleId);
    }

    public void ChangeRole(Guid roleId)
    {
        RoleId = roleId;
    }
}