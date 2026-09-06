using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class EmailConverter()
: ValueConverter<Email, string>(e => e.Value, value => new Email(value));