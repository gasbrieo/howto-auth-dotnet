using HowTo.Auth.Core.Entities;
using HowTo.Auth.Infrastructure.Data;

namespace HowTo.Auth.Presentation.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityConfigurations(this IServiceCollection services)
    {
        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
