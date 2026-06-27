using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Assets;

public record AssetCreatedEvent(Guid AssetId) : DomainEvent;
