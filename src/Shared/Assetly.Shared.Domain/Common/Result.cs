namespace Assetly.Shared.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public ErrorType? ErrorType { get; }

    protected Result(bool isSuccess, Error error, ErrorType? errorType = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error, ErrorType errorType = Common.ErrorType.Failure) =>
        new(false, error, errorType);
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool isSuccess, T? value, Error error, ErrorType? errorType = null)
        : base(isSuccess, error, errorType)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, Error.None);

    public static implicit operator Result<T>(T? value) =>
        value is not null ? Success(value) : Failure(Error.NullValue);

    public static new Result<T> Failure(
        Error error,
        ErrorType errorType = Common.ErrorType.Failure
    ) => new(false, default, error, errorType);
}
