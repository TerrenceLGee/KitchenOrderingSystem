using Auth.Domain.Entities.ValueObjects.User;

using KitchenOrderingSystem.Shared.Common;

using Email = Auth.Domain.Entities.ValueObjects.User.Email;

namespace Auth.Domain.Entities;

public class User : BaseEntity
{
    public FirstName FirstName { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public Challenge Challenge { get; private set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<UserRole> Roles { get; set; } = [];
    
    private User() {}

    private User(
        FirstName firstName,
        LastName lastName,
        Email email,
        Password password,
        Challenge challenge)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Challenge = challenge;
    }

    public static User Create(
        string firstNameValue,
        string lastNameValue,
        string emailAddressValue,
        string passwordValue,
        string questionValue,
        string answerValue)
    {
        var firstName = new FirstName(firstNameValue);
        var lastName = new LastName(lastNameValue);
        var email = new Email(emailAddressValue);
        var password = new Password(passwordValue);
        var challenge = new Challenge(
            new ChallengeQuestion(questionValue), 
            new ChallengeAnswer(answerValue));

        return new User(
            firstName,
            lastName,
            email,
            password,
            challenge);
    }

    public void UpdateFirstName(string firstName)
    {
        FirstName = new FirstName(firstName);
    }

    public void UpdateLastName(string lastName)
    {
        LastName = new LastName(lastName);
    }

    public void UpdateEmailAddress(string emailAddress)
    {
        Email = new Email(emailAddress);
    }

    public void ResetPassword(string password)
    {
        Password = new Password(password);
    }

    public void UpdateChallenge(string question, string answer)
    {
        Challenge = new Challenge(
            new ChallengeQuestion(question), 
            new ChallengeAnswer(answer));
    }
}