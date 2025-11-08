
using Microsoft.EntityFrameworkCore;
using ShopAPI.Application;
using ShopAPI.Domain.Repositories;
using ShopAPI.Infrastructure.Data;
using ShopAPI.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using ShopAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddDbContext<ShopDbContext>(
    options => options.UseMongoDB(connectionString, "shopDatabase") 
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
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();