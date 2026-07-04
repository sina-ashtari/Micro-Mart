using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// this is used for read operations 
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull;
