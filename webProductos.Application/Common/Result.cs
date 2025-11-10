namespace webProductos.Application.Common;

public class Result
{
    public bool Success { get; }
    public string? Error { get; }
    public ResultType Type { get; }

    private Result(ResultType type, bool success, string? error)
    {
        Type = type;
        Success = success;
        Error = error;
    }

    
    public static Result Ok() => new(ResultType.Ok, true, null);
    public static Result NotFound() => new(ResultType.NotFound, false, "Not found");
    public static Result Conflict(string message) => new(ResultType.Conflict, false, message);
    public static Result Failure(string message) => new(ResultType.Failure, false, message);
}


public enum ResultType
{
    Ok, NotFound, Conflict, Failure
}