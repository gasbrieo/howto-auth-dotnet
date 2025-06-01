using HowTo.Auth.Presentation.Tests.TestHelpers;

namespace HowTo.Auth.Presentation.Tests.Features.Health;

public class LivenessTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenHealthCheckConfigured_WhenLivenessCalled_ThenReturnsOk()
    {
        // Given

        // When
        var livenessResponse = await _client.GetAsync("/api/health/liveness");
        var livenessResult = await livenessResponse.Content.ReadFromJsonAsync<HealthCheckResponse>();

        // Then
        Assert.Equal(HttpStatusCode.OK, livenessResponse.StatusCode);
        Assert.NotNull(livenessResult);
        Assert.Equal(HealthStatus.Healthy.ToString(), livenessResult.Status);
        Assert.Null(livenessResult.Dependencies);
    }

    private class HealthCheckResponse
    {
        public string Status { get; set; } = string.Empty;
        public Dictionary<string, string>? Dependencies { get; set; }
    }
}

