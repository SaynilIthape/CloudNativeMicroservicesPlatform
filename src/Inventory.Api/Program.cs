using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Inventory.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var cosmosDbEndpoint = builder.Configuration["CosmosDb:Endpoint"] ?? throw new InvalidOperationException("Cosmos DB endpoint is missing.");
var cosmosDbDatabaseName = builder.Configuration["CosmosDb:DatabaseName"]?? throw new InvalidOperationException("Cosmos DB database name is missing.");
var cosmosDbContainerName = builder.Configuration["CosmosDb:ContainerName"] ?? throw new InvalidOperationException("Cosmos DB container name is missing.");

builder.Services.AddSingleton<CosmosClient>(sp=>
{
    var credential = new DefaultAzureCredential();
    return new CosmosClient(cosmosDbEndpoint, credential);
});

builder.Services.AddSingleton(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    return cosmosClient.GetContainer(cosmosDbDatabaseName, cosmosDbContainerName);
});

builder.Services.AddScoped<IProductService, ProductService>();  

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
