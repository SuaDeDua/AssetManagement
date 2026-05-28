using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Domain.AssetModels;

public static class AssetModelError
{
    public static Error NotFound(Guid assetModelId) =>
        Error.NotFound(
            "AssetModel.NotFound",
            $"The category with the identifier {assetModelId} was not found"
        );

    public static readonly Error AlreadyArchived = Error.Problem(
        "Categories.AlreadyArchived",
        "The category was already archived"
    );
}
