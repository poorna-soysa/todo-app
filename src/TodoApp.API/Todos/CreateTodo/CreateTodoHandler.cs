using MediatR;
using TodoApp.API.Abstractions;

namespace TodoApp.API.Todos.CreateTodo;

public record CreateTodoCommand(string Name) : ICommand<CreateTodoResult>;
public record CreateTodoResult(Guid Id);
internal class CreateTodoCommandHandler
    : ICommandHandler<CreateTodoCommand, CreateTodoResult>
{
    public async Task<CreateTodoResult> Handle(CreateTodoCommand command,
        CancellationToken cancellationToken)
    {

        return new CreateTodoResult(Guid.NewGuid());
    }
}
