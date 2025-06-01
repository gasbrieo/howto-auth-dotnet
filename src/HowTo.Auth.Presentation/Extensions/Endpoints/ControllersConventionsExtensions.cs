using HowTo.Auth.Presentation.Configurations.Conventions;

namespace HowTo.Auth.Presentation.Extensions.Endpoints;

public static class ControllersConventionsExtensions
{
    public static IServiceCollection AddControllersWithConventions(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseTransformer()));
        });

        return services;
    }
}
