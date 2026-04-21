using FluentValidation;

namespace AssetManagement.Modules.Assets.Application.Assets.CreateAsset;

internal sealed class CreateAssetCommandValidator : AbstractValidator<CreateAssetCommand>
{
    public CreateAssetCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.SerialNumber).NotEmpty();
    }
}
