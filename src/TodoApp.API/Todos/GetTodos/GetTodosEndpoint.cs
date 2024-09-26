namespace TodoApp.API.Todos.GetTodos;

public record GetTodosRequest();
public record GetTodosResponse(Guid Id, string Name, bool IsComplete);

public class GetTodosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos", async (ISender sender) =>
        {
            var result = await sender.Send(new GetTodosQuery());

            var response = result.Adapt<IEnumerable<GetTodosResponse>>();

            return Results.Ok(response);
        })
        .WithName("GetTodos")
       .Produces<GetTodosResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get Todos")
       .WithDescription("Get Todos");
    }
}
