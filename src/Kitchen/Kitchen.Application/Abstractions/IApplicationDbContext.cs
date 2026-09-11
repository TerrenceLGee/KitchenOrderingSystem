using Kitchen.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Kitchen.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Ingredient> Ingredients { get; }
    DbSet<Recipe> Recipes { get; }
    DbSet<RecipeIngredient> RecipeIngredients { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}