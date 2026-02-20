using AssetManagement.Domain.Shared.Validation;
using AssetManagement.Shared.Kernel.Common;
using AssetManagement.Shared.Kernel.Events;

namespace AssetManagement.Domain.Shared.Common;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        Guard.AgainstNull(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
