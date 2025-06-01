namespace HowTo.Auth.Presentation.Tests.TestHelpers.Extensions;

public static class FakeAuthenticationExtensions
{
    public static IServiceCollection AddFakeAuthentication(this IServiceCollection services)
    {
        var authDescriptors = services
            .Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>))
            .ToList();

        foreach (var descriptor in authDescriptors)
            services.Remove(descriptor);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = StaticTokenService.Issuer,
                    ValidAudience = StaticTokenService.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticTokenService.SecretKey))
                };
            });
        services.AddAuthorization();

        return services;
    }
}
