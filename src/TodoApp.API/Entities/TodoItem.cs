namespace TodoApp.API.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public DateTime LastUpdatedOnUtc { get; set; } 
}
