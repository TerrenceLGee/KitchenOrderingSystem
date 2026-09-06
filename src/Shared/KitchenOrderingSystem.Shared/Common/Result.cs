using System.Diagnostics.CodeAnalysis;

namespace KitchenOrderingSystem.Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("Success result cannot have an error.");
            case false when error == Error.None:
                throw new InvalidOperationException("Failure result must have an error.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);

    protected static Result<T> Create<T>(T? value) => value is not null
        ? Success(value)
        : Failure<T>(Error.NullValue);
}

public class Result<T> : Result
{
    [NotNull]
    public T Value => IsSuccess
        ? field!
        : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static implicit operator Result<T>(T? value) => Create(value);
}