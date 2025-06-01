using HowTo.Auth.Presentation.Tests.TestHelpers;

namespace HowTo.Auth.Presentation.Tests.Features.Health;

public class ReadinessTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenHealthCheckConfigured_WhenReadinessCalled_ThenReturnsOk()
    {
        // Given

        // When
        var readinessResponse = await _client.GetAsync("/api/health/readiness");
        var readinessResult = await readinessResponse.Content.ReadFromJsonAsync<HealthCheckResponse>();

        // Then
        Assert.Equal(HttpStatusCode.OK, readinessResponse.StatusCode);
        Assert.NotNull(readinessResult);
        Assert.Equal(HealthStatus.Healthy.ToString(), readinessResult.Status);
        Assert.NotNull(readinessResult.Dependencies);
        Assert.Single(readinessResult.Dependencies, p => p.Key == "fake" && p.Value == HealthStatus.Healthy.ToString());
    }

    private class HealthCheckResponse
    {
        public string Status { get; set; } = string.Empty;
        public Dictionary<string, string>? Dependencies { get; set; }
    }
}

