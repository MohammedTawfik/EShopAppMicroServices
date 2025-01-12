using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Utilities.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] Handle request : {typeof(TRequest).Name} - Response : {typeof(TResponse).Name} - Request Date : {request}");

            var timer = new Stopwatch();
            timer.Start();
            var response = await next();
            timer.Stop();
            var elapsed = timer.Elapsed;
            if (elapsed.Seconds > 3)
            {
                logger.LogWarning($"[Performance] The request : {typeof(TRequest).Name} took {elapsed} seconds");
            }

            logger.LogInformation($"[END] Handled request : {typeof(TRequest).Name} with Response : {typeof(TResponse).Name}");
            return response;
        }
    }
}
