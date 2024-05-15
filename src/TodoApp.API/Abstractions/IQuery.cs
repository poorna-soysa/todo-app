using MediatR;

namespace TodoApp.API.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
