using Kitchen.Domain.Entities.ValueObjects.Ingredient;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Ingredient;

public class IngredientNameConverter()
: ValueConverter<IngredientName, string>(n => n.Value, value => new IngredientName(value));