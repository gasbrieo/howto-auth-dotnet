using HowTo.Auth.UseCases.Auth.Login;
using HowTo.Auth.UseCases.Auth.Register;

namespace HowTo.Auth.Presentation.Controllers.V1;

public class AuthController(
    IRegisterUserUseCase registerUserUseCase,
    ILoginUserUseCase loginUserUseCase) : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await registerUserUseCase.ExecuteAsync(request, cancellationToken);
        return ToActionResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var result = await loginUserUseCase.ExecuteAsync(request, cancellationToken);
        return ToActionResult(result);
    }
}
