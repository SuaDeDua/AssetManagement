using Assetly.Shared.Kernel.Events;

namespace Assetly.Shared.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
    public int Version => 1;
}
