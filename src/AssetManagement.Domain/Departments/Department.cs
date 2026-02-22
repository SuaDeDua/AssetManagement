using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Departments;

public sealed class Department : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public HardwareProfile HardwareProfile { get; private set; }
    public Guid CompanyId { get; }
    public Guid CustomerId { get; }
    public Guid LocationId { get; }
    public ImageUrl Image { get; private set; }

    private Department() { }
}
