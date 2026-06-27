using Assetly.Modules.Assets.Application.Common.Data;
using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Shared.Domain.ValueObjects;
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

        modelBuilder.Entity<Asset>(builder =>
        {
            builder
                .Property(a => a.Description)
                .HasConversion(description => description.Value, value => new Description(value));
        });
    }
}
