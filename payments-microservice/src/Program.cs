using Messaging;
using Microsoft.OpenApi.Models;
using PaymentsMicroservice.Application.Services.Implementations;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Implementations;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Repositories.Data;
using PaymentsMicroservice.Repositories.Implementations;

// Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register database context
builder.Services.AddSingleton<MongoDbContext>();

// Register application services
builder.Services.AddScoped<IElectronicBillService, ElectronicBillService>();
builder.Services.AddScoped<IPaymentCodeService, PaymentCodeService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPayerService, PayerService>();

// Register domain services
builder.Services.AddScoped<IElectronicBillDomainService, ElectronicBillDomainService>();
builder.Services.AddScoped<IPaymentCodeDomainService, PaymentCodeDomainService>();
builder.Services.AddScoped<IPaymentDomainService, PaymentDomainService>();

// Register repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IElectronicBillRepository, ElectronicBillRepository>();
builder.Services.AddScoped<IPaymentCodeRepository, PaymentCodeRepository>();
builder.Services.AddScoped<IPayerRepository, PayerRepository>();

// Register messaging services
builder.Services.AddTransient<Publisher>(); // Use AddTransient if instances don't need to be shared
builder.Services.AddTransient<Consumer>();  // Use AddTransient if instances don't need to be shared

// Enable Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments Microservice API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configure Kestrel to listen on all network interfaces
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8007); // This will listen on all network interfaces
});



// App
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGet("/", () => "This is Payments Microservice !!!");


// Run
await app.RunAsync();
