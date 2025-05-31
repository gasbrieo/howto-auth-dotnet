using HowTo.Auth.UseCases.Common;

using IUseCaseResult = HowTo.Auth.UseCases.Common.IResult;

namespace HowTo.Auth.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ToActionResult(IUseCaseResult result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Ok(result.GetValue()),
            ResultStatus.Created => Created(result.Location, result.GetValue()),
            ResultStatus.Error => BadRequest(result.Errors),
            ResultStatus.NoContent => NoContent(),
            ResultStatus.NotFound => NotFoundProblem(result.Errors),
            ResultStatus.Unauthorized => Unauthorized(result.Errors),
            ResultStatus.Forbidden => Forbidden(result.Errors),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };
    }

    protected BadRequestObjectResult BadRequest(IEnumerable<string> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Bad Request",
            Status = StatusCodes.Status400BadRequest
        };

        problemDetails.Extensions.Add("errors", errors.ToArray());

        return new BadRequestObjectResult(problemDetails);
    }

    protected UnauthorizedObjectResult Unauthorized(IEnumerable<string> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Unauthorized",
            Status = StatusCodes.Status401Unauthorized
        };

        problemDetails.Extensions.Add("errors", errors.ToArray());

        return new UnauthorizedObjectResult(problemDetails);
    }

    protected ObjectResult Forbidden(IEnumerable<string> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Forbidden",
            Status = StatusCodes.Status403Forbidden
        };

        problemDetails.Extensions.Add("errors", errors.ToArray());

        return new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }

    protected NotFoundObjectResult NotFoundProblem(IEnumerable<string> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Not Found",
            Status = StatusCodes.Status404NotFound
        };

        problemDetails.Extensions.Add("errors", errors.ToArray());

        return new NotFoundObjectResult(problemDetails);
    }
}
