using TodoApp.API.Abstractions;
using TodoApp.API.Exceptions;

namespace TodoApp.API.Todos.UpdateTodo;

public record UpdateTodoCommand(Guid Id, string Name, bool IsCompleted) : ICommand<UpdateTodoResult>;
public record UpdateTodoResult(bool IsSuccess);
public class UpdateTodoCommandHandler(TodoDb dbContext, ILogger<UpdateTodoCommandHandler> logger)
    : ICommandHandler<UpdateTodoCommand, UpdateTodoResult>
{
    public async Task<UpdateTodoResult> Handle(UpdateTodoCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateTodoCommandHandler.Handle is called with {command}", command);

        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == command.Id);

        if (todo is null)
        {
            throw new NotFoundException(command.Id);
        }

        todo.Name = command.Name;
        todo.IsCompleted = command.IsCompleted; 

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTodoResult(true);
    }
}
