using Kitchen.Domain.Entities.ValueObjects.Recipe;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Recipe;

public class RecipeInstructionsConverter()
: ValueConverter<RecipeInstructions, string>(i => i.Value, value => new RecipeInstructions(value));