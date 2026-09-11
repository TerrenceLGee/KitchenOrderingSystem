using Kitchen.Domain.Entities.ValueObjects.RecipeIngredient;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.RecipeIngredient;

public class IngredientQuantityConverter()
: ValueConverter<IngredientQuantity, decimal>(q => q.Value, value => new IngredientQuantity(value));