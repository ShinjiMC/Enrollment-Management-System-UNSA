using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using users_microservice.services;

var builder = WebApplication.CreateBuilder(args);
var url = builder.Configuration["AppSettings:BaseUrl"];
builder.WebHost.UseUrls(url);
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapGet("/", () => "Hello World!");
app.Run();
