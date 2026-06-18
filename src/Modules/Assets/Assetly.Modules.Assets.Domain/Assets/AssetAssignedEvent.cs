using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Assets;

public record AssetAssignedEvent(Guid AssetId, Guid AssignedUserId) : DomainEvent;
