using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Modules.Assets.Domain.Categories.ValueObjects;

namespace Assetly.Modules.Assets.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name, CategoryType Type) : ICommand<Guid>;
