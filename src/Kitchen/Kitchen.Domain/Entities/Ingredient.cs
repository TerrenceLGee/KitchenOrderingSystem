using Kitchen.Domain.Entities.Enums.Ingredient;
using Kitchen.Domain.Entities.Enums.RecipeIngredient;
using Kitchen.Domain.Entities.ValueObjects.Ingredient;

using KitchenOrderingSystem.Shared.Common;

namespace Kitchen.Domain.Entities;

public class Ingredient : BaseEntity
{
    public IngredientName Name { get; private set; } = null!;
    public IngredientCategory Category { get; private set; }
    public bool IsInStock { get; private set; } = true;
    public Stock QuantityInStock { get; private set; } = null!;
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
    
    private Ingredient() {}

    private Ingredient(
        IngredientName name,
        IngredientCategory category,
        bool isInStock,
        Stock quantityInStock)
    {
        Name = name;
        Category = category;
        IsInStock = isInStock;
        QuantityInStock = quantityInStock;
    }

    public static Ingredient Create(
        string nameValue,
        IngredientCategory category,
        bool isInStock,
        decimal quantityInStockValue,
        UnitOfMeasurement unitOfMeasurement)
    {
        var name = new IngredientName(nameValue);
        var quantityInStock = new Stock(quantityInStockValue, unitOfMeasurement);

        return new Ingredient(
            name,
            category,
            isInStock,
            quantityInStock);
    }

    public void UpdateName(string nameValue)
    {
        Name = new IngredientName(nameValue);
    }

    public void UpdateCategory(IngredientCategory category)
    {
        Category = category;
    }

    public void UpdateStockQuantity(decimal quantityInStock, UnitOfMeasurement unit)
    {
        QuantityInStock = new Stock(quantityInStock, unit);
    }

    public void UpdateStockStatus(bool isInStock)
    {
        IsInStock = isInStock;
    }

    public void IncrementStockQuantity(decimal incrementValue)
    {
        var previousStock = QuantityInStock.Quantity;
        var unit = QuantityInStock.UnitOfMeasurement;
        QuantityInStock = new Stock(incrementValue + previousStock, unit);
        if (QuantityInStock.Quantity > 0) IsInStock = true;
    }

    public void DecrementStockQuantity(decimal decrementValue)
    {
        var previousStock = QuantityInStock.Quantity;
        var updatedStock = previousStock - decrementValue;
        var unit = QuantityInStock.UnitOfMeasurement;
        QuantityInStock = new Stock(updatedStock, unit);
        if (QuantityInStock.Quantity <= 0) IsInStock = false;
    }

    public bool HasEnoughStock(decimal stockValue)
    {
        return QuantityInStock.Quantity >= stockValue;
    }
}