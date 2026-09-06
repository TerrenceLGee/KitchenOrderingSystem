using System.Linq.Dynamic.Core;
using System.Reflection;

namespace KitchenOrderingSystem.Shared.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPagination<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string? sortBy) where T : class
    {
        if (string.IsNullOrWhiteSpace(sortBy)) return query;

        var sortExpressions = new List<string>();

        foreach (var part in sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var tokens = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) continue;

            var propertyPath = tokens[0];

            if (!IsValidPropertyPath(typeof(T), propertyPath)) continue;

            var direction = tokens.Length > 1 && tokens[1]
                .Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? "descending"
                : "ascending";
            
            sortExpressions.Add($"{propertyPath} {direction}");
        }

        return sortExpressions.Count > 0
            ? query.OrderBy(string.Join(", ", sortExpressions))
            : query;
    }

    private static bool IsValidPropertyPath(Type type, string path)
    {
        var parts = path.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var currentType = type;

        foreach (var part in parts)
        {
            var prop = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name.Equals(part, StringComparison.OrdinalIgnoreCase));

            if (prop is null) return false;

            currentType = prop.PropertyType;
        }

        return true;
    }
}