using TodoApp.API.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TodoApp.API.Todos.GetTodos;

public record GetTodosQuery() : IQuery<GetTodosResult>;
public record GetTodosResult(IEnumerable<TodoItem> Todos);
internal class GetTodosQueryHandler(TodoDb dbContext, ILogger<GetTodosQueryHandler> logger)
    : IQueryHandler<GetTodosQuery, GetTodosResult>
{
    public async Task<GetTodosResult> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetTodosQueryHandler.Handle is called with {@Query}", query);

        var todos = await dbContext.TodoItems.ToListAsync(cancellationToken);

        return new GetTodosResult(todos);
    }
}
