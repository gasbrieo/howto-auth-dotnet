using HowTo.Auth.Presentation.Docs.OpenApi;

namespace HowTo.Auth.Presentation.Docs;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.UseCommentsAndDocumentation();
                options.EnableAnnotations();
                options.UseInlineDefinitionsForEnums();
                options.UseFriendlySchemaIds();
            })
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureDescriptionOptions>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSecurityOptions>();
    }

    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
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

    private static void UseCommentsAndDocumentation(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }

    private static void UseFriendlySchemaIds(this SwaggerGenOptions options)
    {
        options.CustomSchemaIds(type =>
        {
            if (type.IsGenericType)
            {
                var genericTypeDef = type.GetGenericTypeDefinition();
                var baseName = genericTypeDef.Name!.Split('`')[0];

                var genericArgs = type.GetGenericArguments()
                    .Select(t => t.Name);

                return $"{baseName}<{string.Join(",", genericArgs)}>";
            }

            return type.Name;
        });
    }
}
