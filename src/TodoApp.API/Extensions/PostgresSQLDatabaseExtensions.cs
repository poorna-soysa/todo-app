namespace TodoApp.API.Extensions;

public static class PostgresSQLDatabaseExtensions
{
    public static IServiceCollection AddPostgresSQLDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<TodoDb>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

        return services;
    }
}
