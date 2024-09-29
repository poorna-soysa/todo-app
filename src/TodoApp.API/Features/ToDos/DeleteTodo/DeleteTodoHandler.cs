namespace TodoApp.API.Features.ToDos.DeleteTodo;

public record DeleteTodoCommand(Guid Id) : ICommand<DeleteTodoResult>;
public record DeleteTodoResult(bool IsSuccess);

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required!");
    }
}

internal class DeleteTodoCommandHandler(
    TodoDb dbContext) :
    ICommandHandler<DeleteTodoCommand, DeleteTodoResult>
{
    public async Task<DeleteTodoResult> Handle(DeleteTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == command.Id);

        if (todo is null)
        {
            throw new NotFoundException(command.Id);
        }

        dbContext.TodoItems.Remove(todo);
        await dbContext.SaveChangesAsync();

        return new DeleteTodoResult(true);
    }
}
