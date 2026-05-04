using MediatR;

namespace Assetly.Modules.Assets.Application.Assets.GetAsset;

public sealed record GetAssetQuery(Guid AssetId) : IRequest<AssetResponse?>;
