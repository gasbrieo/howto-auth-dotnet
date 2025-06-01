using HowTo.Auth.Infrastructure.Data;
using HowTo.Auth.Presentation.Tests.TestHelpers.Extensions;

namespace HowTo.Auth.Presentation.Tests.TestHelpers;

public class FunctionalTestFixture : WebApplicationFactory<IPresentationMarker>, IDisposable
{
    private readonly SqliteConnection _sqliteConnection;

    public FunctionalTestFixture()
    {
        _sqliteConnection = new SqliteConnection("DataSource=:memory:");
        _sqliteConnection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            Environment.SetEnvironmentVariable("JWT_KEY", StaticTokenService.SecretKey);

            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Issuer"] = StaticTokenService.Issuer,
                ["Jwt:Audience"] = StaticTokenService.Audience
            });
        });

        builder.ConfigureServices(services =>
        {
            services
                .AddFakeHealthChecks()
                .AddFakeAuthentication()
                .AddFakeDbContext(_sqliteConnection);
        });

        builder.ConfigureServices((context, services) =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        });
    }

    public void ReinitializeDatabase()
    {
        using var scope = Server.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    public new void Dispose()
    {
        base.Dispose();
        _sqliteConnection?.Dispose();
        GC.SuppressFinalize(this);
    }
}

