using AssetManagement.Domain.Shared.Common;

namespace AssetManagement.Domain.Assets;

public sealed class AssetModel : AggregateRoot<Guid>
{
    public Guid CategoryId { get; }
    public Guid? FieldSetId { get; private set; }

    public void AssignFieldSet(Guid fieldSetId)
    {
        FieldSetId = fieldSetId;
    }
}
