namespace HowTo.Auth.Presentation.Extensions.Endpoints;

public static class JsonOptionsExtensions
{
    public static IServiceCollection AddJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }
}
