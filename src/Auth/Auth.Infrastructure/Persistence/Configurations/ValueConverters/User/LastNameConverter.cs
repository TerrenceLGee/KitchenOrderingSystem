using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class LastNameConverter()
: ValueConverter<LastName, string>(ln => ln.Value, value => new LastName(value));