namespace TodoApp.API.Todos.GetTodoById;

public record GetTodoByIdResponse(Guid Id, string Name, bool IsCompleted);
public class GetTodoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos/{id:guid}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetTodoByIdQuery(Id));

            var response = result.Adapt<GetTodoByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetTodoById")
       .Produces<GetTodoByIdResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Todo By Id")
       .WithDescription("Get Todo By Id");
    }
}
