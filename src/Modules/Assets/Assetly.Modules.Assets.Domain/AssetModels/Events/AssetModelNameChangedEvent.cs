using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.AssetModels.Events;

public record AssetModelNameChangedEvent(Guid AssetModelId, string OldName, string NewName)
    : DomainEvent;
