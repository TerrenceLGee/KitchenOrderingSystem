using Auth.Domain.Entities.ValueObjects.SigningKey;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.SigningKey;

public class PrivateKeyConverter()
: ValueConverter<PrivateKey, string>(pk => pk.Value, value => new PrivateKey(value));