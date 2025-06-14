using HowTo.Auth.Presentation.Tests.TestHelpers;
using HowTo.Auth.UseCases.Auth.Login;
using HowTo.Auth.UseCases.Auth.Register;
using HowTo.Auth.UseCases.Users.GetCurrentUser;

namespace HowTo.Auth.Presentation.Tests.Features.Users;

public class GetCurrentUserTests(FunctionalTestFixture fixture) : IClassFixture<FunctionalTestFixture>
{
    private readonly HttpClient _client = fixture.CreateClient();

    [Fact]
    public async Task GivenValidToken_WhenGetCurrentUserCalled_ThenReturnsOk()
    {
        // Given
        var registerRequest = new RegisterUserRequest { Email = "test@example.com", Password = "Test@123" };
        await _client.PostAsJsonAsync("/api/v1/auth/register", registerRequest);

        var loginRequest = new LoginUserRequest { Email = registerRequest.Email, Password = registerRequest.Password };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginUserResponse>();

        _client.DefaultRequestHeaders.Authorization = new("Bearer", loginResult!.Token);

        // When
        var meResponse = await _client.GetAsync("/api/v1/users/me");
        var meResult = await meResponse.Content.ReadFromJsonAsync<GetCurrentUserResponse>();

        // Then
        Assert.Equal(HttpStatusCode.OK, meResponse.StatusCode);
        Assert.NotNull(meResult);
        Assert.Equal(registerRequest.Email, meResult.Email);
    }

    [Fact]
    public async Task GivenNoToken_WhenGetCurrentUserCalled_ThenReturnsUnauthorized()
    {
        // Given
        _client.DefaultRequestHeaders.Authorization = null;

        // When
        var response = await _client.GetAsync("/api/v1/users/me");

        // Then
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
