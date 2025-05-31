using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.Infrastructure.Data;
using HowTo.Auth.Infrastructure.Data.Repositories;
using HowTo.Auth.Infrastructure.Identity;

namespace HowTo.Auth.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
