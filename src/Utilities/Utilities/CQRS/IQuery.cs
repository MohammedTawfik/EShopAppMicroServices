using MediatR;

namespace Utilities.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
