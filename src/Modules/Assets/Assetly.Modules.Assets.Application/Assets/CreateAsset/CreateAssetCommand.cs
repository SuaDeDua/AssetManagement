using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Shared.Domain.ValueObjects;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

public sealed record CreateAssetCommand(string Name, Description Description, string SerialNumber)
    : ICommand<Guid>;
