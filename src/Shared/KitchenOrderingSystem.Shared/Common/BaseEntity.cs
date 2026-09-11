namespace KitchenOrderingSystem.Shared.Common;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
}