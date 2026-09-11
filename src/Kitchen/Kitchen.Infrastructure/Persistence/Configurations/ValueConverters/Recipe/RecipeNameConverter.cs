using Kitchen.Domain.Entities.ValueObjects.Recipe;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Recipe;

public class RecipeNameConverter() 
: ValueConverter<RecipeName, string>(n => n.Value, value => new RecipeName(value));