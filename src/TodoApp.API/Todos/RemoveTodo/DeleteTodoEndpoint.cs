namespace TodoApp.API.Todos.RemoveTodo;

public record DeleteTodoResponse(bool IsSuccess);

public class DeleteTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/todos/{id:guid}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTodoCommand(Id));

            var response = result.Adapt<DeleteTodoResult>();

            return Results.Ok(response);
        })
        .WithName("DeleteTodo")
       .Produces<DeleteTodoResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Delete Todo by Id")
       .WithDescription("Delete Todo By Id");
    }
}
