using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class ChallengeAnswerConverter()
: ValueConverter<ChallengeAnswer, string>(ca => ca.Value, value => new ChallengeAnswer(value));