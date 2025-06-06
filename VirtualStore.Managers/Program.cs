var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddControllers().AddDapr();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();