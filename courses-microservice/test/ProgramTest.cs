using course_microservice.context;
using course_microservice.repositories;
using course_microservice.services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using Microsoft.AspNetCore.Builder;


namespace course_microservice.test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configura cualquier servicio que necesites aquí
                services.RemoveAll(typeof(MyDbContext));
                services.AddDbContext<MyDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
            });

            return base.CreateHost(builder);
        }
    }

    [TestFixture]
    public class MyWebAppTests
    {
        private CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetRoot_ReturnsExpectedResponse()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("This is Courses Microservice !!!", content);
        }

        [Test]
        public void EnsureServicesAreConfigured()
        {
            // Act
            var scope = _factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            // Assert
            Assert.NotNull(services.GetService<IScheduleService>());
            Assert.NotNull(services.GetService<IScheduleRepository>());
        }
        [Test]
        public void TestGetConnectionString()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:MySqlConnection", "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddConfiguration(configuration);

            // Act
            var connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ??
                throw new InvalidOperationException("Connection string not found in appsettings.json");

            // Assert
            Assert.IsNotNull(connectionString);
            Assert.AreEqual("Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;", connectionString);
        }
    }
}
