using Kitchen.Domain.Entities.ValueObjects.Ingredient;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kitchen.Infrastructure.Persistence.Configurations.ValueConverters.Ingredient;

public class StockConverter()
: ValueConverter<Stock, decimal>(s => s.Quantity, value => new Stock(value));