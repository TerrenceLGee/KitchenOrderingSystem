using Auth.Domain.Entities.ValueObjects.Role;

namespace Auth.Domain.Entities;

public class Role : BaseEntity
{
    public RoleName Name { get; private set; } = null!;
    public RoleDescription Description { get; private set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = [];
    
    private Role() {}

    private Role(RoleName name, RoleDescription description)
    {
        Name = name;
        Description = description;
    }

    public static Role Create(string nameValue, string descriptionValue)
    {
        var name = new RoleName(nameValue);
        var description = new RoleDescription(descriptionValue);

        return new Role(name, description);
    }
}