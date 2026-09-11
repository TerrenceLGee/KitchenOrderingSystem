using Kitchen.Domain.Entities.Enums.Recipe;
using Kitchen.Domain.Entities.ValueObjects.Recipe;

using KitchenOrderingSystem.Shared.Common;

namespace Kitchen.Domain.Entities;

public class Recipe : BaseEntity
{
    public RecipeName Name { get; private set; } = null!;
    public RecipeInstructions Instructions { get; private set; } = null!;
    public RecipeCategory Category { get; private set; }
    public ICollection<RecipeIngredient> Ingredients { get; set; } = [];
    
    private Recipe() {}

    private Recipe(
        RecipeName name, 
        RecipeInstructions instructions, 
        RecipeCategory category)
    {
        Name = name;
        Instructions = instructions;
        Category = category;
    }

    public static Recipe Create(
        string nameValue,
        string instructionsValue,
        RecipeCategory category)
    {
        var name = new RecipeName(nameValue);
        var instructions = new RecipeInstructions(instructionsValue);

        return new Recipe(
            name,
            instructions,
            category);
    }

    public void PrepareRecipe()
    {
        foreach (var recipeIngredient in Ingredients)
        {
            recipeIngredient.Ingredient.DecrementStockQuantity(recipeIngredient.Quantity.Value);
        }
    }
}