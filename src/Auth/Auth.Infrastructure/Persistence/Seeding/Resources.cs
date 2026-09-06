using Auth.Domain.Entities;

namespace Auth.Infrastructure.Persistence.Seeding;

public static class Resources
{
    private const string Password = "Pa$$w0rd";
    
    private static readonly Role[] SeedRoles =
    [
        Role.Create("Admin", "Admin Role"),
        Role.Create("Manager", "Manager Role"),
        Role.Create("Chef", "Chef Role"),
        Role.Create("Customer", "Customer Role")
    ];

    private static readonly User[] SeedUsers =
    [
        User.Create(
            "Marvin",
            "Junior",
            "admin@Example.com",
            HashPassword(Password),
            "What is your mother's maiden name?",
            "Johnson"),

        User.Create(
            "Levi",
            "Stubbs",
            "manager@example.com",
            HashPassword(Password),
            "What is your favorite restaurant?",
            "Red Lobster"),

        User.Create(
            "David",
            "Ruffin",
            "chef@example.com",
            HashPassword(Password),
            "What is your favorite style of cooking?",
            "Gourmet"),

        User.Create(
            "John",
            "Edwards",
            "customer@example.com",
            HashPassword(Password),
            "Who is your favorite singer?",
            "Sam Cooke")
    ];

    public static IEnumerable<Role> GetRolesForSeeding() => SeedRoles;
    public static IEnumerable<User> GetUsersForSeeding() => SeedUsers;

    public static IEnumerable<UserRole> GetUserRolesForSeeding()
    {
        var roles = GetRolesForSeeding().ToArray();
        var users = GetUsersForSeeding().ToArray();

        var adminUserRole = UserRole.Create(users[0].Id, roles[0].Id);
        var managerUserRole = UserRole.Create(users[1].Id, roles[1].Id);
        var chefUserRole = UserRole.Create(users[2].Id, roles[2].Id);
        var customerUserRole = UserRole.Create(users[3].Id, roles[3].Id);

        return [adminUserRole, managerUserRole, chefUserRole, customerUserRole];
    }


    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}