using AssetManagement.Shared.Kernel.Events;

namespace AssetManagement.Domain.Shared.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();

    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;

    public int Version => 1;
}
