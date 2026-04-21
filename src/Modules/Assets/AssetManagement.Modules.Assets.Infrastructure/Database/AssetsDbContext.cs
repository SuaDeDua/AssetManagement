using AssetManagement.Modules.Assets.Application.Data;
using AssetManagement.Modules.Assets.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Modules.Assets.Infrastructure.Database;

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
