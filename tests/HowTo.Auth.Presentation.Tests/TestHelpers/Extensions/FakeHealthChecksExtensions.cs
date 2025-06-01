namespace HowTo.Auth.Presentation.Tests.TestHelpers.Extensions;

public static class FakeHealthChecksExtensions
{
    public static IServiceCollection AddFakeHealthChecks(this IServiceCollection services)
    {
        services.Configure<HealthCheckServiceOptions>(options => options.Registrations.Clear());

        services.AddHealthChecks()
            .AddCheck("fake", () => HealthCheckResult.Healthy());

        return services;
    }
}
