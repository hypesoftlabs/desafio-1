using DotNetEnv;
using FluentValidation.AspNetCore;
using Hypesoft.Application.Interface;
using Hypesoft.Application.Services;
using Hypesoft.Domain.Interfaces;
using Hypesoft.Infrastructure.Config;
using Hypesoft.Infrastructure.Context;
using Hypesoft.Infrastructure.Repository;
using Microsoft.Extensions.Options;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

var mongoSettings = new MongoDbSettings
{
    ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"),
    DatabaseName = Environment.GetEnvironmentVariable("MONGO_DATABASE_NAME")
};
builder.Services.Configure<MongoDbSettings>(opt =>
{
    opt.ConnectionString = mongoSettings.ConnectionString;
    opt.DatabaseName = mongoSettings.DatabaseName;
});

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoDbContext(settings);
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoService = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

    try
    {
        
        var db = mongoService.GetCollection<object>("test");
        Console.WriteLine("✅ Conexão MongoDB realizada com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao conectar ao MongoDB: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
