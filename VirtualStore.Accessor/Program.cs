using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPost("/process-product", async (ProductRequest request) =>
{
    await processProductAsync(request);
    return Results.Ok($"product id:{request.id} confirm: #{Guid.NewGuid().ToString()[..8]}");
});
app.Run();
//Process time
async Task processProductAsync(ProductRequest request)
{
    await Task.Delay(100);
}
//process object type
public record ProductRequest(string name, string id, List<string> items);
