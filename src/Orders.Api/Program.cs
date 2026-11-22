
using Azure.Core;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Orders.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString =
    builder.Configuration.GetConnectionString("OrdersDb");

var credential = new DefaultAzureCredential();

var dataSourceBuilder =
    new NpgsqlDataSourceBuilder(connectionString);

dataSourceBuilder.UsePeriodicPasswordProvider(
    async (_, cancellationToken) =>
    {
        var token = await credential.GetTokenAsync(
            new TokenRequestContext(
                new[] { "https://ossrdbms-aad.database.windows.net/.default" }),
            cancellationToken);

        return token.Token;
    },
    TimeSpan.FromMinutes(55),
    TimeSpan.FromSeconds(10));

var dataSource = dataSourceBuilder.Build();

builder.Services.AddSingleton(dataSource);

builder.Services.AddDbContext<OrdersDbContext>(options =>
{
    options.UseNpgsql(dataSource);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
