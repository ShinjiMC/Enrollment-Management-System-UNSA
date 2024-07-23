using enrollments_microservice.Application.Services.Implementations;
using enrollments_microservice.Application.Services.Interfaces;
using enrollments_microservice.Domain.Repositories;
using enrollments_microservice.Domain.Services.Implementations;
using enrollments_microservice.Domain.Services.Interfaces;
using enrollments_microservice.Repositories.Data;
using enrollments_microservice.Repositories.ExternalServices;
using enrollments_microservice.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>();

if (string.IsNullOrEmpty(mongoDbSettings?.ConnectionString) || string.IsNullOrEmpty(mongoDbSettings?.DatabaseName))
    throw new InvalidOperationException("MongoDB configuration is missing in appsettings.");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddHttpClient<IUserExternalService, UserExternalService>();

builder.Services.AddHttpClient<ISchoolExternalService, SchoolExternalService>();

builder.Services.AddHttpClient<ICoursesExternalService, CoursesExternalService>();

builder.Services.AddScoped<IEnrollService, EnrollService>();

builder.Services.AddScoped<IEnrollServiceDomain, EnrollServiceDomain>();

builder.Services.AddScoped<IEnrollRepository, EnrollRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", () => "This is Enrollment Microservice !!!");

app.Run();
