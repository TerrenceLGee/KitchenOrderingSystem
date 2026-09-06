using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class ChallengeQuestionConverter()
: ValueConverter<ChallengeQuestion, string>(cq => cq.Value, value => new ChallengeQuestion(value));