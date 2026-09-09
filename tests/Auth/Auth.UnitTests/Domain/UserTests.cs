using Auth.Domain.Entities;

using FluentAssertions;

namespace Auth.UnitTests.Domain;

public class UserTests
{
    [Fact]
    public void User_Should_Be_Created_Successfully_When_Input_IsValid()
    {
        var user = User.Create(
            "Johnny",
            "Carter",
            "customer@example.com",
            "Pa$$w0rd",
            "Favorite food?",
            "Pizza");

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.FirstName.Value.Should().Be("johnny");
    }

    [Fact]
    public void User_Should_Not_Be_Created_Successfully_And_ArgumentException_Should_Be_Thrown_When_Password_Is_Empty()
    {
        var act = () => User.Create(
            "Johnny",
            "Carter",
            "customer@example.com",
            "",
            "Favorite food?",
            "Pizza");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Password cannot be null empty or whitespace");
    }

    [Fact]
    public void
        User_Should_Not_Be_Created_Successfully_And_ArgumentException_Should_Be_Throw_When_EmailAddress_IsInvalid()
    {
        var act = () => User.Create(
            "Johnny",
            "Carter",
            "customer?example.com",
            "Pa$$word",
            "Favorite food?",
            "Pizza");

        act.Should().Throw<ArgumentException>()
            .WithMessage("Email address is invalid.");
    }
}