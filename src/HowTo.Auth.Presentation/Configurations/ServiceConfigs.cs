using HowTo.Auth.Infrastructure;
using HowTo.Auth.UseCases;

namespace HowTo.Auth.Presentation.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddUseCasesServices()
            .AddInfrastructureServices(configuration);
    }
}
