namespace HowTo.Auth.UseCases.Auth.Login;

public class LoginUserRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
