using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS;

/// <summary>
/// this command handler returns nothing 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand<Unit>;

/// <summary>
/// this command handler returns a object 
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> where TResponse : notnull;
