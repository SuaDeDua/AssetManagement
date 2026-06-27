using Assetly.Modules.Assets.Application.Common.Messaging;

namespace Assetly.Modules.Assets.Application.Assets.GetAsset;

public sealed record GetAssetQuery(Guid AssetId) : IQuery<AssetResponse>;
