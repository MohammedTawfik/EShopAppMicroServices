using MediatR;

namespace Utilities.CQRS
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }

    //this interface is used when the command does not return a result
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
}
