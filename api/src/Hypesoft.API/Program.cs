using DotNetEnv;
using FluentValidation.AspNetCore;
using Hypesoft.Application;
using Hypesoft.Application.Interface;
using Hypesoft.Application.Mappings;
using Hypesoft.Application.Services;
using Hypesoft.Domain.Interfaces;
using Hypesoft.Infrastructure.Config;
using Hypesoft.Infrastructure.Context;
using Hypesoft.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/hypersoft-realm/protocol/openid-connect/token";
    options.Audience = "hypersoft-api";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://keycloak:8080/realms/hypersoft-realm",
        ValidAudience = "hypersoft-api"

    };
});
builder.Services.AddAuthorization();


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssembly(Assembly.Load("Hypesoft.Application")));


builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(ProductMapping).Assembly);


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),                     
        Assembly.Load("Hypesoft.Application"),               
        Assembly.Load("Hypesoft.Domain")                     
    );
});

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

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
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.Run();
