using MediatR;

namespace TodoApp.API.Abstractions;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
     where TQuery : IQuery<TResponse>
     where TResponse : notnull
{
}

