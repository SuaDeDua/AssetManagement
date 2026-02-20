using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Locations;

public sealed class Location : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string ImageUrl { get; private set; }
}
