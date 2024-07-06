using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using course_microservice.context;
using course_microservice.repositories;
using course_microservice.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Context
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ??
    throw new InvalidOperationException("Connection string not found in appsettings.json");

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


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


builder.Services.AddScoped<IWeekDayRepository, WeekDayRepository>();

builder.Services.AddScoped<IWeekDayService, WeekDayService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();

builder.Services.AddScoped<ISchoolService, SchoolService>();

builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();

builder.Services.AddScoped<IScheduleService, ScheduleService>();

// Build app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "This is Courses Microservice !!!");

app.Run();
