using Auth.Domain.Entities.ValueObjects.Role;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.Role;

public class RoleDescriptionConverter()
: ValueConverter<RoleDescription, string>(rd => rd.Value, value => new RoleDescription(value));