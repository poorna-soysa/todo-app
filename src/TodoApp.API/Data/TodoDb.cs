using Microsoft.EntityFrameworkCore;
using TodoApp.API.Model;

namespace TodoApp.API.Data;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
