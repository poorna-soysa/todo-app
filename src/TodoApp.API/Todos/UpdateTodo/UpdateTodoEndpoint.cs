namespace TodoApp.API.Todos.UpdateTodo;

public record UpdateTodoRequest(Guid Id, string Name, bool IsCompleted);
public record UpdateTodoResponse(bool IsSuccess);

public class UpdateTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/todos/{id:guid}", async (Guid id, UpdateTodoRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateTodoCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateTodoResponse>();

            return Results.Ok(response);
        })
       .WithName("UpdateTodo")
       .Produces<UpdateTodoResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Update Todo")
       .WithDescription("Update Todo");
    }
}
