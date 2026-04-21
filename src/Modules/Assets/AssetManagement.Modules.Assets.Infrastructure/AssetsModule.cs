using AssetManagement.Modules.Assets.Application;
using AssetManagement.Modules.Assets.Application.Data;
using AssetManagement.Modules.Assets.Domain.Assets;
using AssetManagement.Modules.Assets.Infrastructure.Assets;
using AssetManagement.Modules.Assets.Infrastructure.Data;
using AssetManagement.Modules.Assets.Infrastructure.Database;
using AssetManagement.Modules.Assets.Presentation.Assets;
using AssetManagement.Shared.Application;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace AssetManagement.Modules.Assets.Infrastructure;

public static class AssetsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        AssetEndpoints.MapEnpoints(app);
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
