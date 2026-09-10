using Auth.Domain.Entities;

namespace Auth.UnitTests.TestResources;

public static class UserFactory
{
    public static User Create()
    {
        return User.Create(
            "Johnny",
            "Carter",
            "jcarter@example.com",
            HashPassword("Pa$$w0rd"),
            "Favorite food?",
            "Pizza");
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}