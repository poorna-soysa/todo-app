using TodoApp.API.Entities;

namespace TodoApp.API.Data;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
