using Kitchen.Domain.Entities.Enums.RecipeIngredient;
using Kitchen.Domain.Entities.ValueObjects.RecipeIngredient;

namespace Kitchen.Domain.Entities;

public class RecipeIngredient
{
    public Guid RecipeId { get; private set; }
    public Recipe Recipe { get; set; } = null!;
    public Guid IngredientId { get; private set; }
    public Ingredient Ingredient { get; set; } = null!;
    public IngredientQuantity Quantity { get; private set; } = null!;
    public UnitOfMeasurement UnitOfMeasurement { get; private set; }
    
    private RecipeIngredient() {}

    private RecipeIngredient(
        Guid recipeId,
        Guid ingredientId,
        IngredientQuantity quantity,
        UnitOfMeasurement unitOfMeasurement)
    {
        RecipeId = recipeId;
        IngredientId = ingredientId;
        Quantity = quantity;
        UnitOfMeasurement = unitOfMeasurement;
    }

    public static RecipeIngredient Create(
        Guid recipeId,
        Guid ingredientId,
        decimal quantityValue,
        UnitOfMeasurement unitOfMeasurement)
    {
        var quantity = new IngredientQuantity(quantityValue);

        return new RecipeIngredient(
            recipeId, 
            ingredientId, 
            quantity, 
            unitOfMeasurement);
    }

    public void UpdateQuantity(decimal quantity)
    {
        Quantity = new IngredientQuantity(quantity);
    }

    public void IncrementQuantity(decimal quantityToIncrement)
    {
        var currentQuantity = Quantity.Value;
        var updatedQuantity = currentQuantity + quantityToIncrement;
        Quantity = new IngredientQuantity(updatedQuantity);
    }

    public void DecrementQuantity(decimal quantityToDecrement)
    {
        var currentQuantity = Quantity.Value;
        var updatedQuantity = (currentQuantity - quantityToDecrement);
        var sanitizedUpdatedQuantity = updatedQuantity < 0
            ? 0
            : updatedQuantity;
        Quantity = new IngredientQuantity(sanitizedUpdatedQuantity);
    }
}