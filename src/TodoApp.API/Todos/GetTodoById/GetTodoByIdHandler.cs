namespace TodoApp.API.Todos.GetTodoById;

public record GetTodoByIdQuery(Guid Id) : IQuery<GetTodoByIdResult>;
public record GetTodoByIdResult(Guid Id, string Name, bool IsCompleted);

internal class GetTodoByIdQueryHandler(TodoDb dbContext)
    : IQueryHandler<GetTodoByIdQuery, GetTodoByIdResult>
{
    public async Task<GetTodoByIdResult> Handle(GetTodoByIdQuery query,
        CancellationToken cancellationToken)
    {
        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(query.Id);
        }

        var result = todo.Adapt<GetTodoByIdResult>();

        return result;
    }
}
