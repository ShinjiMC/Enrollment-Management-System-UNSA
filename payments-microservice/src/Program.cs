// Builder
using PaymentsMicroservice.Domain.Repositories;
using PaymentsMicroservice.Infrastructure.Repositories;
using PaymentsMicroservice.Services.Implementations;
using PaymentsMicroservice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register application services
builder.Services.AddScoped<IPaymentService, PaymentService>();
// builder.Services.AddScoped<IInvoiceService, InvoiceService>(); // Asegúrate de tener IInvoiceService y su implementación
// builder.Services.AddScoped<IPaymentCodeService, PaymentCodeService>(); // Asegúrate de tener IPaymentCodeService y su implementación

// Register domain repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentCodeRepository, PaymentCodeRepository>();

// App
var app = builder.Build();
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.MapControllers();
app.MapGet("/", () => "This is Payments Microservice !!!");

app.Run();
