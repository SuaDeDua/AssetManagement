using Assetly.Api;
using Assetly.Api.Extensions;
using Assetly.Modules.Assets.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter()
    );
});

builder.Services.AddAssetModule(builder.Configuration);

builder.Services.AddSingleton<Assetly.Shared.Kernel.Common.ICurrentUser, FakeCurrentUser>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigration();
}

AssetsModule.MapEndpoints(app);

app.UseHttpsRedirection();

await app.RunAsync();
