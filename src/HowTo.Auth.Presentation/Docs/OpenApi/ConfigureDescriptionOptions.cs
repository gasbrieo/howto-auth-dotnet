namespace HowTo.Auth.Presentation.Docs.OpenApi;

public class ConfigureDescriptionOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo()
            {
                Title = "HowTo.Auth",
                Version = description.ApiVersion.ToString(),
            };

            options.SwaggerDoc(description.GroupName, info);
        }
    }
}
