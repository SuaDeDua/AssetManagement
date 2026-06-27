using Assetly.Shared.Domain.Events;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Domain.Assets;

public record AssetUpdateDetailsDomainEvent(Guid AssetId, Description Description) : DomainEvent;
