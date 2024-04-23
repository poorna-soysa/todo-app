namespace TodoApp.API.Todos.GetTodos;

public record GetTodosQuery() : IQuery<GetTodosResult>;
public record GetTodosResult(IEnumerable<TodoItem> Todos);
internal class GetTodosQueryHandler(TodoDb dbContext)
    : IQueryHandler<GetTodosQuery, GetTodosResult>
{
    public async Task<GetTodosResult> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        var todos = await dbContext.TodoItems.ToListAsync(cancellationToken);

        return new GetTodosResult(todos);
    }
}
