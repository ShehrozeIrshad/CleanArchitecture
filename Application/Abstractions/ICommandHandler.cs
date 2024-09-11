﻿using MediatR;

namespace Application.Abstractions
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
     where TCommand : ICommand<TResponse>
    {
    }
}