using System.Security.Claims;
using HowTo.Auth.UseCases.Common;
using HowTo.Auth.UseCases.Me.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;

namespace HowTo.Auth.Presentation.Controllers.V1;

[Authorize]
public class MeController(IGetCurrentUserUseCase getCurrentUserUseCase) : BaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(GetCurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
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
