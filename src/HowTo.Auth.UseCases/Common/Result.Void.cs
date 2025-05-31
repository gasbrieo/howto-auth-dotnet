namespace HowTo.Auth.UseCases.Common;

public class Result : Result<Result>
{
    public Result() : base() { }

    protected internal Result(ResultStatus status) : base(status) { }

    public static Result<TValue> Success<TValue>(TValue value) => new(value);

    public static Result<TValue> Created<TValue>(TValue value, string location) => Result<TValue>.Created(value, location);

    public new static Result NoContent() => new(ResultStatus.NoContent);

    public new static Result Error(params string[] errors) => new(ResultStatus.Error)
    {
        Errors = errors
    };

    public new static Result Unauthorized(params string[] errors) => new(ResultStatus.Unauthorized)
    {
        Errors = errors
    };

    public new static Result Forbidden(params string[] errors) => new(ResultStatus.Forbidden)
    {
        Errors = errors
    };

    public new static Result NotFound(params string[] errors) => new(ResultStatus.NotFound)
    {
        Errors = errors
    };
}
