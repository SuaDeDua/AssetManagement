using FluentValidation;

namespace Assetly.Modules.Assets.Application.Assets.CreateAsset;

internal sealed class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(x => x.AssetModelId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.SerialNumber).NotEmpty();
        RuleFor(x => x.AssetModelId).NotEmpty();
    }
}
