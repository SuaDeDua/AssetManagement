namespace AssetManagement.Domain.Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public ErrorType? ErrorType { get; }

    protected Result(bool isSuccess, string error, ErrorType? errorType = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorType = errorType;
    }

    public static Result Success() => new(true, string.Empty);

    public static Result Failure(string error, ErrorType errorType = Common.ErrorType.Failure) =>
        new(false, error, errorType);
}

public class Result<T> : Result
{
    public T? Value { get; }

    protected Result(bool isSuccess, T? value, string error, ErrorType? errorType = null)
        : base(isSuccess, error, errorType)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, string.Empty);

    public static new Result<T> Failure(
        string error,
        ErrorType errorType = Common.ErrorType.Failure
    ) => new(false, default, error, errorType);
}
