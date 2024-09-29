namespace TodoApp.API.Features.ToDos.CreateTodo;

public record CreateTodoRequest(string Name);
public record CreateTodoResponse(Guid Id);
public class CreateTodoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/todos", async (CreateTodoRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateTodoCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateTodoResponse>();

            return Results.Created($"/todos/{response.Id}", response);
        })
        .WithName("CreateTodo")
        .Produces<CreateTodoResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Todo")
        .WithDescription("Create Todo");
    }
}
