using NotificationsMicroservice.Domain.Services.Interfaces;
using NotificationsMicroservice.Domain.Repositories;
using NotificationsMicroservice.Repositories.Implementations;
using NotificationsMicroservice.Repositories.Data;
using NotificationsMicroservice.Application.Services.Interfaces;
using NotificationsMicroservice.Application.Services.Implementations;
using NotificationsMicroservice.Domain.Services.Implementations;  // Asegúrate de incluir este using
using Microsoft.OpenApi.Models;

// Builder
var builder = WebApplication.CreateBuilder(args);

// Register services for DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications Microservice", Version = "v1" });
});

// MongoDB Context
builder.Services.AddSingleton<MongoDbContext>();

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register domain services
builder.Services.AddScoped<IUserDomainService, UserDomainService>();
builder.Services.AddScoped<INotificationDomainService, NotificationDomainService>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifications Microservice v1"));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Welcome to Notifications Microservice!");

app.Run();
