using TodoApp.API.Models;

namespace TodoApp.API;

public static class EndpointExtensions
{
    private const string TodoEndpoint = "/todos";
    public static void AddTodoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(TodoEndpoint, async (TodoDb db) =>
       await db.TodoItems.ToListAsync());

        app.MapGet($"{TodoEndpoint}/{{id}}", async (int id, TodoDb db) =>
               await db.TodoItems.FindAsync(id));

        app.MapGet($"{TodoEndpoint}/complete", async (TodoDb db) =>
               await db.TodoItems.Where(d => d.IsCompleted).ToListAsync());

        app.MapPost(TodoEndpoint, async (CreateTodoItemDto request, TodoDb db) =>
        {
            TodoItem todo = new() { Name = request.Name };

            db.TodoItems.Add(todo);
            await db.SaveChangesAsync();

            return Results.Created($"{TodoEndpoint}/{todo.Id}", todo);
        });

        app.MapPut($"{TodoEndpoint}/{{id}}", async (int id, UpdateTodoItemDto request, TodoDb db) =>
        {
            var todo = await db.TodoItems.FindAsync(id);

            if (todo is null)
            {
                return Results.NotFound();
            }

            todo.Name = request.Name;
            todo.IsCompleted = request.IsCompleted;
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

        app.MapDelete($"{TodoEndpoint}/{{id}}", async (int id, TodoDb db) =>
        {
            var todo = await db.TodoItems.FindAsync(id);

            if (todo is null)
            {
                return Results.NotFound();
            }

            db.TodoItems.Remove(todo);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
