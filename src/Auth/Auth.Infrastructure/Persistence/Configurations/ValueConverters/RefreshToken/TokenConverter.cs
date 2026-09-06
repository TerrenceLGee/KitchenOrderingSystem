using Auth.Domain.Entities.ValueObjects.RefreshToken;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.RefreshToken;

public class TokenConverter()
: ValueConverter<Token, string>(t => t.Value, value => new Token(value));