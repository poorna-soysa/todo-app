
using FluentValidation;
using TodoApp.API.Behaviors;
using TodoApp.API.Exceptions.Handlers;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services.AddDbContext<TodoDb>(options =>
            options.UseInMemoryDatabase("TodoList"));

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Add todo endpoints 
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
