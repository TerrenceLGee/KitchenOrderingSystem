using System.Data;

using Auth.Domain.Entities;

using FluentAssertions;

namespace Auth.UnitTests.Domain;

public class RoleTests
{
    [Fact]
    public void Role_Created_Successfully_When_Input_IsValid()
    {
        var role = Role.Create(
            "Line Cook",
            "Line cook role");

        role.Should().NotBeNull();
        role.Id.Should().NotBeEmpty();
        role.Name.Value.Should().Be("line cook");
    }

    [Fact]
    public void Role_Should_Not_Be_Created_And_Throws_ArgumentException_When_Input_IsInvalid()
    {
        var act = () => Role.Create(
            "",
            "Unknown role");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Role name cannot be null empty or whitespace");
    }
}