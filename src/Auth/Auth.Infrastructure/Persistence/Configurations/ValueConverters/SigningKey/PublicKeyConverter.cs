using Auth.Domain.Entities.ValueObjects.SigningKey;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.SigningKey;

public class PublicKeyConverter()
: ValueConverter<PublicKey, string>(pk => pk.Value, value => new PublicKey(value));