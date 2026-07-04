using MediatR;

namespace BuildingBlocks.CQRS;

// Unit is a void type since void is not a valid return type
public interface ICommand : ICommand<Unit>;

/// <summary>
/// this interface return a response 
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>;
