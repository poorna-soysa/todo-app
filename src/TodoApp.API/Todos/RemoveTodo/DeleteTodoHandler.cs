using TodoApp.API.Abstractions;
using TodoApp.API.Exceptions;
using TodoApp.API.Todos.CreateTodo;

namespace TodoApp.API.Todos.RemoveTodo;

public record DeleteTodoCommand(Guid Id) : ICommand<DeleteTodoResult>;
public record DeleteTodoResult(bool IsSuccess);

internal class DeleteTodoCommandHandler(
    TodoDb dbContext, ILogger<DeleteTodoCommandHandler> logger
    ) :
    ICommandHandler<DeleteTodoCommand, DeleteTodoResult>
{
    public async Task<DeleteTodoResult> Handle(DeleteTodoCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteTodoCommandHandler.Handle is called with {command}", command);


        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == command.Id);

        if (todo is null)
        {
            throw new TodoNotFoundException(command.Id);
        }

        dbContext.TodoItems.Remove(todo);
        await dbContext.SaveChangesAsync();

        return new DeleteTodoResult(true);
    }
}
