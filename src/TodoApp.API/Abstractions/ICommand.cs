using MediatR;

namespace TodoApp.API.Abstractions;

public interface ICommand: ICommand<Unit>
{   

}
public interface ICommand<out TResponse>: IRequest<TResponse>
{
}
