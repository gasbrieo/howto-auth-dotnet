using HowTo.Auth.Presentation.Extensions.Endpoints;

namespace HowTo.Auth.Presentation.Extensions;

public static class EndpointConfigurationExtensions
{
    public static IServiceCollection AddEndpointConfigurations(this IServiceCollection services)
    {
        services
            .AddControllersWithConventions()
            .AddRouteOptions()
            .AddJsonOptions()
            .AddApiVersioningWithExplorer();

        return services;
    }
}
