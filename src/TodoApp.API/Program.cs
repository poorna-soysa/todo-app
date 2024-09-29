var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

builder.Services
    .AddPostgresSQLDatabase(builder.Configuration)
    .AddMediatRConfiguration(assembly)
    .AddValidatorsFromAssembly(assembly)
    .AddCarter()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails()    
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
