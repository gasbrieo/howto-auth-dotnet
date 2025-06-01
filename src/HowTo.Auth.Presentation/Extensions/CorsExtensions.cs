namespace HowTo.Auth.Presentation.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsConfigurations(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsConfigurations(this IApplicationBuilder app)
    {
        app.UseCors("AllowAll");

        return app;
    }
}
