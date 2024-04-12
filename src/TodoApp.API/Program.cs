using Microsoft.EntityFrameworkCore;
using TodoApp.API;
using TodoApp.API.Data;
using TodoApp.API.Dtos;
using TodoApp.API.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Add todo endpoints 
app.AddTodoEndpoints();

app.Run();
