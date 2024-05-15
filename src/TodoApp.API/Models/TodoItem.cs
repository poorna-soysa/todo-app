namespace TodoApp.API.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
