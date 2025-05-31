namespace HowTo.Auth.UseCases.Common;

public class Result<TValue> : IResult
{
    public TValue? Value { get; init; }

    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    public bool IsSuccess => Status is ResultStatus.Ok;

    public IEnumerable<string> Errors { get; protected set; } = [];

    public string Location { get; protected set; } = string.Empty;

    protected Result() { }

    public Result(TValue? value) => Value = value;

    protected Result(ResultStatus status) => Status = status;

    public object? GetValue() => Value;

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Created(TValue value, string location) => new(ResultStatus.Created)
    {
        Value = value,
        Location = location
    };

    public static Result<TValue> NoContent() => new(ResultStatus.NoContent);

    public static Result<TValue> Error(params string[] errors) => new(ResultStatus.Error)
    {
        Errors = errors
    };

    public static Result<TValue> Unauthorized(params string[] errors) => new(ResultStatus.Unauthorized)
    {
        Errors = errors
    };

    public static Result<TValue> Forbidden(params string[] errors) => new(ResultStatus.Forbidden)
    {
        Errors = errors
    };

    public static Result<TValue> NotFound(params string[] errors) => new(ResultStatus.NotFound)
    {
        Errors = errors
    };

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Result result) => new(default(TValue))
    {
        Status = result.Status,
        Errors = result.Errors,
    };
}
