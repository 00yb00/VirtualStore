using Dapr.Client;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
//Post endpoint
app.UseHttpsRedirection();

app.MapPost("/create-product", async (ProductRequest request, DaprClient client) =>
{
    var result = await client.InvokeMethodAsync<ProductRequest, string>("accessor-api", "process-product", request);
    return result;
});
app.Run();
public record ProductRequest(string name, string id, List<string> items);

