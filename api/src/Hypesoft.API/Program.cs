using Swashbuckle.AspNetCore.SwaggerGen;
using Hypesoft.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80); // Escuta na porta 80 dentro do container
});

builder.Services.AddOpenApi();

// MongoDB settings
builder.Services.Configure<Hypesoft.API.Models.MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);
builder.Services.AddSingleton<Hypesoft.API.Services.MongoDbService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger Settings
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Hypesoft v1");
    });
}

app.UseHttpsRedirection();

app.UseCors();

// app.UseAuthorization();

app.MapControllers();

app.Run();