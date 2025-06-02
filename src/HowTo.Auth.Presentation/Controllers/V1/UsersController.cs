using HowTo.Auth.UseCases.Common;
using HowTo.Auth.UseCases.Users.GetCurrentUser;

namespace HowTo.Auth.Presentation.Controllers.V1;

[Authorize]
public class UsersController(IGetCurrentUserUseCase getCurrentUserUseCase) : BaseController
{
    [HttpGet("me")]
    [ProducesResponseType(typeof(GetCurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return ToActionResult(Result.Unauthorized("Invalid token or user context."));
        }

        var request = new GetCurrentUserRequest
        {
            UserId = userId,
        };

        var result = await getCurrentUserUseCase.ExecuteAsync(request, cancellationToken);

        return ToActionResult(result);
    }
}
