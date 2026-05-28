using Assetly.Modules.Assets.Domain.Categories.Events;
using Assetly.Modules.Assets.Domain.Categories.ValueObjects;
using Assetly.Shared.Domain.Common;
using Assetly.Shared.Domain.Validation;

namespace Assetly.Modules.Assets.Domain.Categories;

public sealed class Category : AggregateRoot<Guid>
{
    public string Name { get; private set; } = null!;

    public CategoryType Type { get; init; }

    private Category() { }

    private Category(string name, CategoryType type)
    {
        Guard.AgainstNullOrEmpty(name);
        Guard.AgainstInvalidEnum(type);

        Id = Guid.CreateVersion7();
        Name = name;
        Type = type;
    }

    public static Category Create(string name, CategoryType type)
    {
        var category = new Category(name, type);

        category.AddDomainEvent(new CategoryCreatedEvent(category.Id));

        return category;
    }

    public Result ChangeName(string newName)
    {
        Guard.AgainstNullOrEmpty(newName);

        if (Name == newName)
            return Result.Success();

        string oldName = Name;

        Name = newName;

        AddDomainEvent(new CategoryNameChangedEvent(Id, oldName, newName));

        return Result.Success();
    }
}
