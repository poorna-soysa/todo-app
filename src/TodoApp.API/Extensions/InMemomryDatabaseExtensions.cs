namespace TodoApp.API.Extensions;

public static class InMemomryDatabaseExtensions
{
    public static IServiceCollection AddInMemomeryDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TodoDb>(options =>
            options.UseInMemoryDatabase("TodoList"));

        return services;
    }
}
