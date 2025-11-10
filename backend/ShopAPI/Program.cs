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
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/shopapi-.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

Log.Information("Iniciando a Shop API...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var externalAuthority = builder.Configuration["Keycloak:AuthorityExternal"];
    var internalAuthority = builder.Configuration["Keycloak:AuthorityInternal"];
    var audience = builder.Configuration["Keycloak:Audience"];


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
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("FrontendPolicy", policy =>
        {
            policy
                .WithOrigins("http://localhost:3000") 
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

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
                    AuthorizationUrl = new Uri($"{externalAuthority}/protocol/openid-connect/auth"),
                    TokenUrl = new Uri($"{externalAuthority}/protocol/openid-connect/token"),
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
         options.MetadataAddress = $"{internalAuthority}/.well-known/openid-configuration";
         options.RequireHttpsMetadata = false;

         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidIssuer = externalAuthority,

             ValidateAudience = true,
             ValidAudience = audience,

             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,

             NameClaimType = "preferred_username",
             RoleClaimType = ClaimTypes.Role
         };

         options.Events = new JwtBearerEvents
         {
             OnTokenValidated = context =>
             {
                 var identity = context.Principal?.Identity as ClaimsIdentity;
                 if (identity == null)
                     return Task.CompletedTask;

                 // realm_access.roles
                 var realmAccess = identity.FindFirst("realm_access")?.Value;
                 if (!string.IsNullOrEmpty(realmAccess))
                 {
                     using var doc = JsonDocument.Parse(realmAccess);
                     if (doc.RootElement.TryGetProperty("roles", out var rolesElement))
                     {
                         foreach (var role in rolesElement.EnumerateArray())
                         {
                             var roleName = role.GetString();
                             if (!string.IsNullOrEmpty(roleName))
                             {
                                 identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                             }
                         }
                     }
                 }

                 // resource_access.shop-api.roles
                 var resourceAccess = identity.FindFirst("resource_access")?.Value;
                 if (!string.IsNullOrEmpty(resourceAccess))
                 {
                     using var doc = JsonDocument.Parse(resourceAccess);
                     if (doc.RootElement.TryGetProperty("shop-api", out var clientElement) &&
                         clientElement.TryGetProperty("roles", out var clientRolesElement))
                     {
                         foreach (var role in clientRolesElement.EnumerateArray())
                         {
                             var roleName = role.GetString();
                             if (!string.IsNullOrEmpty(roleName))
                             {
                                 identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                             }
                         }
                     }
                 }

                 return Task.CompletedTask;
             }
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
        options.Configuration = "shop-redis:6379";
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
    app.UseCors("FrontendPolicy");
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
