using Auth.Domain.Entities;

namespace Auth.UnitTests.Domain.Resources;

public static class UserResources
{
    public static User GetUserToAddToInMemoryDatabase()
    {
        return User.Create(
            "Johnny",
            "Carter",
            "jcarter@example.com",
            "Pa$$w0rd",
            "Favorite food?",
            "Pizza");
    }
}