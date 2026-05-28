using Assetly.Modules.Assets.Application.Common.Data;
using Assetly.Modules.Assets.Application.Common.Messaging;
using Assetly.Modules.Assets.Domain.Categories;
using Assetly.Shared.Domain.Common;

namespace Assetly.Modules.Assets.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var category = Category.Create(request.Name, request.Type);

        categoryRepository.Insert(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
