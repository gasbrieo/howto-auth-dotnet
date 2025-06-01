using HowTo.Auth.Core.Entities;
using HowTo.Auth.Infrastructure.Data;

namespace HowTo.Auth.Infrastructure.Tests.TestHelpers;

public class IntegrationTestFixture : IDisposable
{
    private readonly SqliteConnection _sqliteConnection;

    public ApplicationDbContext DbContext { get; private set; }
    public UserManager<ApplicationUser> UserManager { get; private set; }

    public IntegrationTestFixture()
    {
        _sqliteConnection = new SqliteConnection("DataSource=:memory:");
        _sqliteConnection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(_sqliteConnection));

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var serviceProvider = services.BuildServiceProvider();

        DbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        DbContext.Database.EnsureCreated();

        UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    }

    public void ReinitializeDatabase()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext?.Dispose();
        UserManager?.Dispose();
        _sqliteConnection?.Dispose();
        GC.SuppressFinalize(this);
    }
}
