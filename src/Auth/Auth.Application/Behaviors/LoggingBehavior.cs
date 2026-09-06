using MediatR;

using Microsoft.Extensions.Logging;

namespace Auth.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull, IRequest<TResponse> 
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handling request: {RequestType} - RequestData: {@Request}",
            typeof(TRequest).Name, request);

        var response = await next(cancellationToken);
        
        logger.LogInformation("[END] Handled request: {RequestType} - Response: {@Response}",
            typeof(TRequest).Name, response);

        return response;
    }
}