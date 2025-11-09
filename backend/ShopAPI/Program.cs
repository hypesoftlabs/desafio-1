
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application;
using ShopAPI.Domain.Repositories;
using ShopAPI.Infrastructure.Data;
using ShopAPI.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using ShopAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/shopapi-.txt", rollingInterval: RollingInterval.Day) 
    .CreateBootstrapLogger();

Log.Information("Iniciando a Shop API...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .WriteTo.Console()
            .WriteTo.File("logs/shopapi-.txt", rollingInterval: RollingInterval.Day);
    });

    var connectionString = builder.Configuration.GetConnectionString("MongoDb");

    builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

 
    builder.Services.AddDbContext<ShopDbContext>(
        (sp, options) => 
        {
            var client = sp.GetRequiredService<IMongoClient>(); 
            options.UseMongoDB(client, "shopDatabase");
        }
    );


    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly));
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.AddProfile<ShopAPI.Application.Mappings.MappingProfile>();
    });
    builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ShopAPI.Application.Behaviors.ValidationBehavior<,>));


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {

                AuthorizationCode = new OpenApiOAuthFlow
                {

                    AuthorizationUrl = new Uri($"{builder.Configuration["Keycloak:Authority"]}/protocol/openid-connect/auth"),

                    TokenUrl = new Uri($"{builder.Configuration["Keycloak:Authority"]}/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID" },
                    { "profile", "Profile" }
                }
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new List<string>()
        }
    });
    });
    builder.Services.AddAuthentication(options =>
    {

        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {

        options.Authority = builder.Configuration["Keycloak:Authority"];


        options.Audience = builder.Configuration["Keycloak:Audience"];

        options.RequireHttpsMetadata = false;


        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true
        };

    });

    builder.Services.AddAuthorization();

    builder.Services.AddHealthChecks()
         .AddMongoDb(
             sp => sp.GetRequiredService<IMongoClient>(),
             name: "mongodb",
             timeout: TimeSpan.FromSeconds(5)
         );
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
        options.InstanceName = "ShopAPI_"; 
    });


    var app = builder.Build();
    app.UseMiddleware<ExceptionHandlingMiddleware>();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {

            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1");


            options.OAuthClientId("shop-api");


            options.OAuthAppName("Shop API - Swagger UI");

            options.OAuthUsePkce();
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapHealthChecks("/health");
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "A aplicação falhou ao iniciar.");
}
finally
{
    Log.CloseAndFlush();
}




