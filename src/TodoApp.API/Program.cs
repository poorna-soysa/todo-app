var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services
    .AddInMemomeryDatabase()
    .AddMediatRConfiguration(assembly)
    .AddCarter()
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddProblemDetails()
    .AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Add todo endpoints 
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
