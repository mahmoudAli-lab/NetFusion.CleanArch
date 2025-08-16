public abstract record Result<T>
{
    public static Result<T> Success(T value) => new SuccessResult<T>(value);
    public static Result<T> Failure(Error error) => new FailureResult<T>(error);
    public static Result<T> Failure(string message) => new FailureResult<T>(new Error(message));
}

public sealed record SuccessResult<T>(T Value) : Result<T>;
public sealed record FailureResult<T>(Error Error) : Result<T>;

// Example pattern matching usage
public async Task<IActionResult> GetUser(int id)
{
    var result = await _mediator.Send(new GetUserQuery(id));
    
    return result switch
    {
        SuccessResult<User> success => Ok(success.Value),
        FailureResult<User> failure when failure.Error.Type == ErrorType.NotFound => NotFound(),
        FailureResult<User> failure => BadRequest(failure.Error.Message),
        _ => throw new InvalidOperationException()
    };
}
