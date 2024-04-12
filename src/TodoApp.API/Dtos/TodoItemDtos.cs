namespace TodoApp.API.Dtos;

public record CreateTodoItemDto(string Name);
public record UpdateTodoItemDto(string Name,bool IsCompleted);

