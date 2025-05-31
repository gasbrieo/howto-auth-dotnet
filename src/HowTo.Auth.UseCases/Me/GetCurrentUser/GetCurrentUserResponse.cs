namespace HowTo.Auth.UseCases.Me.GetCurrentUser;

public class GetCurrentUserResponse
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
}
