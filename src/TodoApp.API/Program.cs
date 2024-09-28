var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services
    .AddInMemomeryDatabase()
    .AddMediatRConfiguration(assembly)
    .AddCarter()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()
    .AddValidatorsFromAssembly(assembly)
    .AddHealthChecks();

var app = builder.Build();

// Add todo endpoints 
app.MapCarter();
app.UseExceptionHandler(options => { });

app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });

app.Run();
