using Assetly.Shared.Domain.Events;

namespace Assetly.Modules.Assets.Domain.Categories.Events;

public record CategoryNameChangedEvent(Guid CategoryId, string OldName, string NewName)
    : DomainEvent;
