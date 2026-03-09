using AssetManagement.Domain.Assets.ValueObjects;
using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class CustomField : Entity<Guid>
{
    public Name Name { get; private set; }
    public Element Element { get; private set; }

    public CustomField(Guid id, Name name, Element element)
    {
        Id = id;
        Name = name;
        Element = element;
    }
}
