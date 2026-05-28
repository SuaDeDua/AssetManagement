using Assetly.Shared.Kernel.Events;

namespace Assetly.Modules.Assets.Domain.AssetModels.Events;

public record AssetModelCreatedEvent(Guid AssetModelId) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    public int Version { get; }
}
