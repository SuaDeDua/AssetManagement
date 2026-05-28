using Assetly.Modules.Assets.Domain.AssetModels.Events;
using Assetly.Modules.Assets.Domain.Categories;
using Assetly.Modules.Assets.Domain.Manufacturers;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.Validation;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Domain.AssetModels;

public sealed class AssetModel : AggregateRoot<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string ModelNo { get; private set; } = string.Empty;
    public Description Description { get; private set; }
    public Guid ManufacturerId { get; }
    public Guid CategoryId { get; }

    private AssetModel() { }

    private AssetModel(
        string name,
        string modelNo,
        Guid manufacturerId,
        Guid categoryId,
        Description description
    )
    {
        Guard.AgainstNullOrEmpty(name);
        Guard.AgainstNullOrEmpty(modelNo);
        Guard.AgainstEmptyGuid(manufacturerId);
        Guard.AgainstEmptyGuid(categoryId);

        Id = Guid.CreateVersion7();
        Name = name;
        ModelNo = modelNo;
        ManufacturerId = manufacturerId;
        CategoryId = categoryId;
        Description = description;
    }

    public static AssetModel Create(
        string name,
        string modelNo,
        Manufacturer manufacturer,
        Category category,
        Description description
    )
    {
        var assetModel = new AssetModel(name, modelNo, manufacturer.Id, category.Id, description);

        assetModel.AddDomainEvent(new AssetModelCreatedEvent(assetModel.Id));
        return assetModel;
    }

    public Result ChangeName(string newName)
    {
        Guard.AgainstNullOrEmpty(newName);

        if (Name == newName)
            return Result.Success();

        string oldName = Name;

        Name = newName;

        AddDomainEvent(new AssetModelNameChangedEvent(Id, oldName, newName));

        return Result.Success();
    }
}
