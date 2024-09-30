namespace TodoApp.API.Features.ToDos.UpdateTodo;

public record UpdateTodoCommand(Guid Id, string Name, bool IsCompleted) : ICommand<UpdateTodoResult>;
public record UpdateTodoResult(bool IsSuccess);
public class UpdateTodoCommandHandler(TodoDb dbContext)
    : ICommandHandler<UpdateTodoCommand, UpdateTodoResult>
{
    public async Task<UpdateTodoResult> Handle(UpdateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == command.Id);

        if (todo is null)
        {
            throw new NotFoundException(command.Id);
        }

        todo.Name = command.Name;
        todo.IsCompleted = command.IsCompleted;
        todo.LastUpdatedOnUtc = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateTodoResult(true);
    }
}
