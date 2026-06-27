using Assetly.Modules.Assets.Application.Common.Data;
using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Application.Assets.UpdateAsset;

internal sealed class UpdateAssetCommandHandler(
    IAssetRepository assetRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateAssetCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        UpdateAssetCommand request,
        CancellationToken cancellationToken
    )
    {
        Asset? asset = await assetRepository.GetAsync(request.AssetId, cancellationToken);

        if (asset is null)
        {
            return Result<Guid>.Failure(AssetErrors.NotFound(request.AssetId));
        }

        asset.UpdateDetails(request.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(asset.Id);
    }
}
