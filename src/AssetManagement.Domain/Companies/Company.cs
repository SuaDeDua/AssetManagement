using AssetManagement.Domain.Shared.Common;

namespace AssetManagement.Domain.Companies;

public sealed class Company : AggregateRoot<Guid>
{
    private Company() { }
}
