using HowTo.Auth.Infrastructure;
using HowTo.Auth.UseCases;

namespace HowTo.Auth.Presentation.Extensions;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddUseCasesServices()
            .AddInfrastructureServices(configuration);
    }
}
