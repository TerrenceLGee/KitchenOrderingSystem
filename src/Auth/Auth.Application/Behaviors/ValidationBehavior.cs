using FluentValidation;

using MediatR;

using Microsoft.Extensions.Logging;

using ValidationException = FluentValidation.ValidationException;

namespace Auth.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        logger.LogInformation("Validating request {RequestType}", typeof(TRequest).Name);

        var context = new ValidationContext<TRequest>(request);
        var validationResults =
            await Task.WhenAll(validators
                .Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .ToList();

        if (failures.Count == 0)
        {
            return await next(cancellationToken);
        }

        logger.LogWarning("Validation failed for {RequestType} with {FailureCount} errors",
            typeof(TRequest).Name, failures.Count);
        throw new ValidationException(failures);
    }
}