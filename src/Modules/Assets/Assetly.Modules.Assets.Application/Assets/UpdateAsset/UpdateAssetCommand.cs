using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Application.Assets.UpdateAsset;

public sealed record UpdateAssetCommand(Guid AssetId, Description Description) : ICommand<Guid>;
