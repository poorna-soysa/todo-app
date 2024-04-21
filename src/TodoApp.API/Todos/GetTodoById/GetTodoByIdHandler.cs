using TodoApp.API.Abstractions;
using TodoApp.API.Exceptions;

namespace TodoApp.API.Todos.GetTodoById;

public record GetTodoByIdQuery(Guid Id) : IQuery<GetTodoByIdResult>;
public record GetTodoByIdResult(TodoItem Todo);

internal class GetTodoByIdQueryHandler(TodoDb dbContext, ILogger<GetTodoByIdQueryHandler> logger)
    : IQueryHandler<GetTodoByIdQuery, GetTodoByIdResult>
{
    public async Task<GetTodoByIdResult> Handle(GetTodoByIdQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("GetTodoByIdQueryHandler.Handle is called with {@Query}", query);

        var todo = await dbContext
            .TodoItems
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);

        if (todo is null)
        {
            throw new NotFoundException(query.Id);
        }

        return new GetTodoByIdResult(todo);
    }
}
