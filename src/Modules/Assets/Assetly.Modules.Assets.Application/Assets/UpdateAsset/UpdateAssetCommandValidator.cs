using FluentValidation;

namespace Assetly.Modules.Assets.Application.Assets.UpdateAsset;

internal sealed class UpdateAssetCommandValidator : AbstractValidator<UpdateAssetCommand>
{
    public UpdateAssetCommandValidator()
    {
        RuleFor(x => x.AssetId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
