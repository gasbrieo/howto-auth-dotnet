using HowTo.Auth.Infrastructure.Data;

namespace HowTo.Auth.Presentation.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheckConfigurations(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("database");

        return services;
    }

    public static IApplicationBuilder UseHealthCheckEndpoints(this IApplicationBuilder app)
    {
        return app
            .UseHealthChecks("/api/health/liveness", new HealthCheckOptions
            {
                Predicate = _ => false,
                ResponseWriter = HealthCheckResponseWriter(includeDependencies: false)
            })
            .UseHealthChecks("/api/health/readiness", new HealthCheckOptions
            {
                ResponseWriter = HealthCheckResponseWriter(includeDependencies: true)
            });
    }

    private static Func<HttpContext, HealthReport, Task> HealthCheckResponseWriter(bool includeDependencies)
    {
        return async (context, report) =>
        {
            var result = new
            {
                status = report.Status.ToString(),
                dependencies = includeDependencies ? report.Entries.ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value.Status.ToString()
                ) : null
            };

            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsJsonAsync(result);
        };
    }
}
