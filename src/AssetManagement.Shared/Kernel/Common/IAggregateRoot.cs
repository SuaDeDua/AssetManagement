using AssetManagement.Shared.Kernel.Events;

namespace AssetManagement.Shared.Kernel.Common;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
