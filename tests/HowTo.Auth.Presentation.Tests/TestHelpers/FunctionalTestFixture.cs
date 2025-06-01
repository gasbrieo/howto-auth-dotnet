using HowTo.Auth.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace HowTo.Auth.Presentation.Tests.TestHelpers;

public class FunctionalTestFixture : WebApplicationFactory<Program>, IDisposable
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

            var config = new Dictionary<string, string?>
            {
                ["JWT_KEY"] = StaticTokenService.SecretKey,
                ["Jwt:Issuer"] = StaticTokenService.Issuer,
                ["Jwt:Audience"] = StaticTokenService.Audience
            };

            configBuilder.AddInMemoryCollection(config);
        });

        builder.ConfigureServices(services =>
        {
            FakeAuthentication(services);
            FakeDbContext(services);
        });

        builder.ConfigureServices((context, services) =>
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        });
    }

    private static void FakeAuthentication(IServiceCollection services)
    {
        var authDescriptors = services
            .Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>))
            .ToList();

        foreach (var descriptor in authDescriptors)
        {
            services.Remove(descriptor);
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = StaticTokenService.Issuer,
                    ValidAudience = StaticTokenService.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticTokenService.SecretKey))
                };
            });

        services.AddAuthorization();
    }

    private void FakeDbContext(IServiceCollection services)
    {
        var descriptorsToRemove = services
            .Where(s => s.ServiceType == typeof(ApplicationDbContext) ||
                        s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(_sqliteConnection));
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
