using AssetManagement.Domain.Locations.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Locations;

public sealed class Location : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public Address Address { get; private set; }
    public City City { get; private set; }
    public ImageUrl Image { get; private set; }
}
