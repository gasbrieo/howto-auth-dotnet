using HowTo.Auth.Presentation.Configurations.SwaggerOptions;

namespace HowTo.Auth.Presentation.Configurations;

public static class SwaggerConfigs
{
    public static IServiceCollection AddSwaggerConfigs(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureDescriptionOptions>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSecurityOptions>();
    }

    public static IApplicationBuilder UseSwaggerConfigs(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        return app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.EnablePersistAuthorization();

                foreach (var groupName in provider.ApiVersionDescriptions.Select(d => d.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpper());
                }
            });
    }
}