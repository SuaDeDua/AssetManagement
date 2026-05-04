using Assetly.Modules.Assets.Application;
using Assetly.Modules.Assets.Application.Data;
using Assetly.Modules.Assets.Domain.Assets;
using Assetly.Modules.Assets.Infrastructure.Assets;
using Assetly.Modules.Assets.Infrastructure.Data;
using Assetly.Modules.Assets.Infrastructure.Database;
using Assetly.Modules.Assets.Presentation.Assets;
using Assetly.Shared.Application.Data;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Assetly.Modules.Assets.Infrastructure;

public static class AssetsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        AssetEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddAssetModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly)
        );

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);

        return services;
    }

    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(
            databaseConnectionString
        ).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddDbContext<AssetsDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName,
                            Schemas.Assets
                        )
                )
                .UseSnakeCaseNamingConvention()
        );
        services.AddScoped<IAssetRepository, AssetRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AssetsDbContext>());
    }
}
