using AssetManagement.Domain.Shared.Common;
using AssetManagement.Domain.Shared.ValueObjects;

namespace AssetManagement.Domain.Assets;

public sealed class FieldSet : AggregateRoot<Guid>
{
    public Name Name { get; private set; }

    private readonly List<FieldSetItem> _items = [];
    public IReadOnlyCollection<FieldSetItem> Items => _items.AsReadOnly();

    private FieldSet() { }

    private FieldSet(Guid id, Name name, IEnumerable<CustomField> initialFields)
    {
        Id = id;
        Name = name;

        int order = 1;
        foreach (var field in initialFields)
        {
            _items.Add(new FieldSetItem(Guid.CreateVersion7(), Id, field.Id, order++));
        }
    }

    public static Result<FieldSet> Create(Name name, IEnumerable<CustomField> initialFields)
    {
        return Result<FieldSet>.Success(new FieldSet(Guid.CreateVersion7(), name, initialFields));
    }

    public void AddFieldItem(Guid customFieldId, int order)
    {
        if (order <= 0)
            throw new ArgumentException("Order must be greater than zero.");

        if (_items.Any(x => x.CustomFieldId == customFieldId))
            return;

        _items.Add(new FieldSetItem(Guid.CreateVersion7(), Id, customFieldId, order));
    }
}

public class FieldSetItem : Entity<Guid>
{
    public Guid FieldSetId { get; private set; }
    public Guid CustomFieldId { get; private set; }
    public int Order { get; private set; }

    private FieldSetItem() { }

    internal FieldSetItem(Guid id, Guid fieldSetId, Guid customFieldId, int order)
    {
        Id = id;
        FieldSetId = fieldSetId;
        CustomFieldId = customFieldId;
        Order = order;
    }
}
