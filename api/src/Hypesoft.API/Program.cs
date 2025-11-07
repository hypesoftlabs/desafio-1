using DotNetEnv;
using FluentValidation.AspNetCore;
using Hypesoft.Application.Interface;
using Hypesoft.Application.Services;
using Hypesoft.Application;
using Hypesoft.Domain.Interfaces;
using Hypesoft.Infrastructure.Config;
using Hypesoft.Infrastructure.Context;
using Hypesoft.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;
using Hypesoft.Application.Mappings;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// ✅ MongoDB Settings
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

// ✅ Injeção de dependências
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ✅ FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssembly(Assembly.Load("Hypesoft.Application")));

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(ProductMapping).Assembly);

// ✅ MediatR - Registro dos handlers
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),                     // API
        Assembly.Load("Hypesoft.Application"),                // Application (onde estão Handlers e Queries)
        Assembly.Load("Hypesoft.Domain")                      // Opcional
    );
});

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Testa conexão com o MongoDB
using (var scope = app.Services.CreateScope())
{
    var mongoService = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    try
    {
        var db = mongoService.GetCollection<object>("test");
        Console.WriteLine("✅ Conexão MongoDB realizada com sucesso!");
        Console.WriteLine($"📦 MongoDB Database: {mongoSettings.DatabaseName}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao conectar ao MongoDB: {ex.Message}");
    }
}

// ✅ Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
