namespace HowTo.Auth.Presentation.Extensions.Endpoints;

public static class RouteOptionsExtensions
{
    public static IServiceCollection AddRouteOptions(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        return services;
    }
}
