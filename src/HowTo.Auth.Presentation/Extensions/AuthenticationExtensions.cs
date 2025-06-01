namespace HowTo.Auth.Presentation.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = Environment.GetEnvironmentVariable("JWT_KEY")!;
                var issuer = configuration["Jwt:Issuer"]!;
                var audience = configuration["Jwt:Audience"]!;

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
