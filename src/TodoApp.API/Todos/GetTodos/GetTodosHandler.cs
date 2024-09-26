using System.Collections.Generic;

namespace TodoApp.API.Todos.GetTodos;

public record GetTodosQuery() : IQuery<IEnumerable<GetTodosResult>>;
public record GetTodosResult(Guid Id, string Name, bool IsCompleted);
internal class GetTodosQueryHandler(TodoDb dbContext)
    : IQueryHandler<GetTodosQuery, IEnumerable<GetTodosResult>>
{
    public async Task<IEnumerable<GetTodosResult>> Handle(
        GetTodosQuery query, 
        CancellationToken cancellationToken)
    {
        var todos = await dbContext
            .TodoItems
            .ToListAsync(cancellationToken);

        var result = todos.Adapt<IEnumerable<GetTodosResult>>();

        return result;
    }
}
