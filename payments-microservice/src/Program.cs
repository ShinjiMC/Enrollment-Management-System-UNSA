using PaymentsMicroservice.Application.Services.Implementations;
using PaymentsMicroservice.Application.Services.Interfaces;
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Domain.Services.Implementations;
using PaymentsMicroservice.Domain.Services.Interfaces;
using PaymentsMicroservice.Infrastructure.Repositories;
// Builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register Domain Services
builder.Services.AddScoped<IPaymentProcessingService, PaymentProcessingService>();

// Register application services
builder.Services.AddScoped<IPaymentService, PaymentService>();

// Register repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IElectronicBillRepository, ElectronicBillRepository>();
builder.Services.AddScoped<IPaymentCodeRepository, PaymentCodeRepository>();

// App
var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "This is Payments Microservice !!!");

app.Run();
