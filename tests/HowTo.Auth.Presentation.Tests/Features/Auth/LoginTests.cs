using HowTo.Auth.Presentation.Tests.TestHelpers;
using HowTo.Auth.UseCases.Auth.Login;
using HowTo.Auth.UseCases.Auth.Register;

namespace HowTo.Auth.Presentation.Tests.Features.Auth;

public class LoginTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenValidCredentials_WhenLoginCalled_ThenReturnsOk()
    {
        // Given
        var registerRequest = new RegisterUserRequest { Email = "test@example.com", Password = "Test@123" };
        await _client.PostAsJsonAsync("/api/v1/auth/register", registerRequest);

        // When
        var loginRequest = new LoginUserRequest { Email = "test@example.com", Password = "Test@123" };
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var result = await response.Content.ReadFromJsonAsync<LoginUserResponse>();

        // Then
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
    }

    [Fact]
    public async Task GivenInvalidCredentials_WhenLoginCalled_ThenReturnsUnauthorized()
    {
        // Given
        var loginRequest = new LoginUserRequest { Email = "nonexistent@example.com", Password = "password123" };

        // When
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var result = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        // Then
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(result);
    }
}
