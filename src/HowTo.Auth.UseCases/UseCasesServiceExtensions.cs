using HowTo.Auth.UseCases.Auth.Login;
using HowTo.Auth.UseCases.Auth.Register;
using HowTo.Auth.UseCases.Me.GetCurrentUser;

namespace HowTo.Auth.UseCases;

public static class UseCasesServiceExtensions
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IGetCurrentUserUseCase, GetCurrentUserUseCase>();

        return services;
    }
}
