using KitchenOrderingSystem.Shared.Common;

namespace Auth.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        result.CheckResult();

        (int statusCode, string title) = MapErrorType(result.Error.ErrorType);

        return Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: result.Error.Description,
            extensions: new Dictionary<string, object?> { ["errorCode"] = result.Error.Code });
    }
    
    private static (int statusCode, string title) MapErrorType(ErrorType type) => type switch
    {
        ErrorType.Validation => (StatusCodes.Status400BadRequest, "Validation Error"),
        ErrorType.BadRequest => (StatusCodes.Status400BadRequest, "Bad Request"),
        ErrorType.NotFound => (StatusCodes.Status404NotFound, "Not Found"),
        ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflict"),
        ErrorType.Unauthorized => (StatusCodes.Status401Unauthorized, "Unauthorized"),
        _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
    };

    private static void CheckResult(this Result result)
    {
        if (result.IsSuccess || result.Error == Error.None)
            throw new InvalidOperationException("Cannot convert a successful result to a problem detail.");
    }
}