using HowTo.Auth.Presentation.Tests.TestHelpers;
using HowTo.Auth.UseCases.Auth.Register;

namespace HowTo.Auth.Presentation.Tests.Features.Auth;

public class RegisterTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenValidRegistrationData_WhenRegisterCalled_ThenReturnsOk()
    {
        // Given
        var registerRequest = new RegisterUserRequest { Email = "newuser@example.com", Password = "Test@123" };

        // When
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", registerRequest);
        var result = await response.Content.ReadFromJsonAsync<RegisterUserResponse>();

        // Then
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
    }

    [Fact]
    public async Task GivenInvalidRegistrationData_WhenRegisterCalled_ThenReturnsBadRequest()
    {
        // Given
        var registerRequest = new RegisterUserRequest { Email = "invalid-email", Password = "short" };

        // When
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", registerRequest);
        var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(result);
    }
}
