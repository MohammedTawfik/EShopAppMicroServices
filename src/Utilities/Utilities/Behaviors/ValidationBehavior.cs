using FluentValidation;
using MediatR;
using Utilities.CQRS;

namespace Utilities.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)                  //use ienumerable to get all validators attached to the command
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResult.Result.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
