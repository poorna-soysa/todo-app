
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(options => options.UseInMemoryDatabase("TodoList"));
builder.Services.AddCarter();

var app = builder.Build();

// Add todo endpoints 
app.MapCarter();

app.Run();
