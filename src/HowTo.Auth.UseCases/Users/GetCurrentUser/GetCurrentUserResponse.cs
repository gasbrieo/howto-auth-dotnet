namespace HowTo.Auth.UseCases.Users.GetCurrentUser;

public class GetCurrentUserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
}
