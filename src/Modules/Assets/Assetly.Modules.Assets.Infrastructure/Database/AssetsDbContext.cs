using Assetly.Modules.Assets.Application.Data;
using Assetly.Modules.Assets.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace Assetly.Modules.Assets.Infrastructure.Database;

public sealed class AssetsDbContext(DbContextOptions<AssetsDbContext> options)
    : DbContext(options),
        IUnitOfWork
{
    internal DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Assets);
    }
}
