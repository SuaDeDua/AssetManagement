using Assetly.Shared.Kernel.Events;

namespace Assetly.Modules.Assets.Domain.Assets;

public record AssetCreatedEvent(Guid AssetId, string Name, string Description, string SerialNumber)
    : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    public int Version { get; }
}
