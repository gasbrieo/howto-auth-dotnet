using HowTo.Auth.Infrastructure.Data;

namespace HowTo.Auth.Presentation.Tests.TestHelpers.Extensions;

public static class FakeDbContextExtensions
{
    public static IServiceCollection AddFakeDbContext(this IServiceCollection services, SqliteConnection connection)
    {
        var descriptorsToRemove = services
            .Where(s => s.ServiceType == typeof(ApplicationDbContext) ||
                        s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
            services.Remove(descriptor);

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));

        return services;
    }
}
