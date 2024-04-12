namespace TodoApp.API.Model;

public class TodoItem
{
    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
