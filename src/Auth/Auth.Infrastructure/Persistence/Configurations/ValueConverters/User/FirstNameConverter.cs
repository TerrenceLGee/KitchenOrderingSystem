using Auth.Domain.Entities.ValueObjects.User;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.User;

public class FirstNameConverter()
: ValueConverter<FirstName, string>(fn => fn.Value, value => new FirstName(value));