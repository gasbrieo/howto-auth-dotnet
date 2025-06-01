namespace HowTo.Auth.UseCases.Common;

public interface IResult
{
    ResultStatus Status { get; }
    IEnumerable<string> Errors { get; }
    object? GetValue();
    string Location { get; }
}
