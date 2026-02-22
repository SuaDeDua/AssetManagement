using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Companies;

public sealed class Company : AggregateRoot<Guid>
{
    public Name Name { get; private set; }

    private Company() { }
}
