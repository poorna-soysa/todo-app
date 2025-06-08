using System.Collections.Generic;

namespace TodoApp.API.Features.ToDos.GetTodos;

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
            .TagWith($"GetTodos - {DateTime.UtcNow}")
            .ToListAsync(cancellationToken);

        var result = todos.Adapt<IEnumerable<GetTodosResult>>();

        return result;
    }
}
