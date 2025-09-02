using Hypesoft.Application.Interfaces;
using Hypesoft.Infrastructure.Data;
using Hypesoft.Infrastructure.Repositories;
using Hypesoft.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hypesoft.Infrastructure.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add MongoDB
        services.AddSingleton<MongoDbContext>();
        
        // Add Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        // Add Services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        
        // Add AutoMapper
        services.AddAutoMapper(typeof(DependencyInjection));
        
        // Add MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Hypesoft.Application.DependencyInjection).Assembly));
        
        // Add FluentValidation
        services.AddValidatorsFromAssembly(typeof(Hypesoft.Application.DependencyInjection).Assembly);
        
        return services;
    }
}
