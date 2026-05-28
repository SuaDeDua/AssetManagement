using Assetly.Modules.Assets.Application.Common.Messaging;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

public sealed record CreateAssetCommand(Guid AssetModelId, string Description, string SerialNumber)
    : ICommand<Guid>;
