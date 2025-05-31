namespace HowTo.Auth.Presentation.Configurations;

public static class CorsConfigs
{
    public static IServiceCollection AddCorsConfigs(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.SetIsOriginAllowed(_ => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCorsConfigs(this IApplicationBuilder app)
    {
        app.UseCors("AllowAllOrigins");

        return app;
    }
}
