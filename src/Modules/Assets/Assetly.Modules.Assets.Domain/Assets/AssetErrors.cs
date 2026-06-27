using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Domain.Assets;

public static class AssetErrors
{
    public static Error NotFound(Guid assetId) =>
        Error.NotFound("Asset.NotFound", $"The asset with the indentifier {assetId} was not found");

    public static readonly Error InvalidStatus = Error.Problem(
        "Asset.InvalidStatus",
        "Cannot update the asset while it is in the"
    );
}
