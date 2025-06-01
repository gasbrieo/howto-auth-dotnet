namespace HowTo.Auth.Presentation.Extensions.Endpoints;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApiVersioningWithExplorer(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new(1, 0);
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        return services;
    }
}
