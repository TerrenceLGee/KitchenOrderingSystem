using Auth.Domain.Entities.ValueObjects.Role;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auth.Infrastructure.Persistence.Configurations.ValueConverters.Role;

public class RoleNameConverter() 
: ValueConverter<RoleName, string>(rn => rn.Value, value => new RoleName(value));