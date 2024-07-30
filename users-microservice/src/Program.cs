using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using users_microservice.Repository.Data;
using users_microservice.Domain.Repository;
using users_microservice.Repository.Mysql;
using users_microservice.Repository.ExternalService;
using users_microservice.Application.Service.Interface;
using users_microservice.Application.Service.Implementations;
using users_microservice.Domain.Services.Interfaces;
using users_microservice.Domain.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Register HttpClient
builder.Services.AddHttpClient();

// Context
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ??
throw new InvalidOperationException("Connection string not found in appsettings.json");

// Add MySql context
builder.Services.AddDbContext<MySqlIdentityContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


// JWT settings
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

// Authentication to swagger settings
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// CORS settings with allow all of the origin localhost:5173
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Repositories
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepositoryImpl>();


builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminServiceDomain, AdminServiceDomainImpl>();
builder.Services.AddScoped<IStudentServiceDomain, StudentServiceDomainImpl>();
builder.Services.AddScoped<IExternalService, ExternalServiceImpl>();
builder.Services.AddScoped<IExternalServiceAuth, ExternalServiceAuthImpl>();



// Build app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "This is Users Microservice !!!");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MySqlIdentityContext>();
    dbContext.Database.Migrate();
}

app.Run();
