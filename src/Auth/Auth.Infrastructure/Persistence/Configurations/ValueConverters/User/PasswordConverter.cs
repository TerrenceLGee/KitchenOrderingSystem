using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class PasswordConverter()
: ValueConverter<Password, string>(p => p.Value, value => new Password(value));