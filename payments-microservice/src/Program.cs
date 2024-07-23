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


// Register domain services
builder.Services.AddScoped<IElectronicBillDomainService, ElectronicBillDomainService>();

// Register repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IElectronicBillRepository, ElectronicBillRepository>();
builder.Services.AddScoped<IPaymentCodeRepository, PaymentCodeRepository>();
builder.Services.AddScoped<IPayerRepository, PayerRepository>();

// App
var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "This is Payments Microservice !!!");

app.Run();
