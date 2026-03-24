namespace AssetManagement.Shared.Domain.Common;

public abstract class Entity<TId> : AuditedEntity
{
    public TId Id { get; protected set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> entity)
            return false;

        if (ReferenceEquals(this, entity))
            return true;

        if (GetType() != entity.GetType())
            return false;

        return !EqualityComparer<TId>.Default.Equals(Id, default!)
            && EqualityComparer<TId>.Default.Equals(Id, entity.Id);
    }

    public override int GetHashCode() =>
        Id is null ? 0 : EqualityComparer<TId>.Default.GetHashCode(Id);
}
