using HowTo.Auth.Presentation.Tests.TestHelpers;

namespace HowTo.Auth.Presentation.Tests.Features.Swagger;

public class SwaggerTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenSwaggerConfigured_WhenSwaggerUICalled_ThenReturnsOk()
    {
        // Given

        // When
        var swaggerResponse = await _client.GetAsync("/swagger");

        // Then
        Assert.Equal(HttpStatusCode.OK, swaggerResponse.StatusCode);
    }

    [Fact]
    public async Task GivenSwaggerConfiguredWithVersioning_WhenSwaggerCalled_ThenReturnsOk()
    {
        // Given
        var apiVersionProvider = fixture.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        var apiVersions = apiVersionProvider.ApiVersionDescriptions;

        // When & Then
        foreach (var apiVersion in apiVersions)
        {
            var swaggerResponse = await _client.GetAsync($"/swagger/{apiVersion.GroupName}/swagger.json");
            Assert.Equal(HttpStatusCode.OK, swaggerResponse.StatusCode);
        }
    }
}
