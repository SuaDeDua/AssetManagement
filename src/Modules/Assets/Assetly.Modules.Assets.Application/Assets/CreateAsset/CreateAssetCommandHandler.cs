using Assetly.Modules.Assets.Application.Common.Data;
using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Modules.Assets.Domain.AssetModels;
using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

internal sealed class CreateAssetCommandHandler(
    IAssetModelRepository assetModelRepository,
    IAssetRepository assetRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateAssetCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateAssetCommand request,
        CancellationToken cancellationToken
    )
    {
        AssetModel? assetModel = await assetModelRepository.GetAsync(
            request.AssetModelId,
            cancellationToken
        );

        if (assetModel is null)
        {
            return Result<Guid>.Failure(AssetModelError.NotFound(request.AssetModelId));
        }

        var result = Asset.Create(
            assetModel.Id,
            assetModel.Name,
            request.Description,
            request.SerialNumber
        );

        assetRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
