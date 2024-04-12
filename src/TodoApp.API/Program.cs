using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Dtos;
using TodoApp.API.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

app.MapGet("/todos", async (TodoDb db) =>
       await db.TodoItems.ToListAsync());

app.MapGet("/todos/{id}", async (int id, TodoDb db) =>
       await db.TodoItems.FindAsync(id));

app.MapPost("/todos", async (CreateTodoItemDto request, TodoDb db) =>
{
    TodoItem todo = new() { Name = request.Name };

    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", async (int id, UpdateTodoItemDto request, TodoDb db) =>
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

app.MapDelete("/todos/{id}", async (int id, TodoDb db) =>
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

app.Run();
