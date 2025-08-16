using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NetFusion.Infrastructure.Persistence;

namespace NetFusion.IntegrationTests;

public abstract class TestBase : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient Client;

    protected TestBase(WebApplicationFactory<Program> factory)
    {
        // Replace SQL Server with InMemory SQLite for tests
        var webFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        });

        Client = webFactory.CreateClient();
    }
}
