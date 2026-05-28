namespace Assetly.Modules.Assets.Domain.Manufacturers;

public interface IManufacturerRepository
{
    Task<Manufacturer?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Manufacturer manufacturer);
}
