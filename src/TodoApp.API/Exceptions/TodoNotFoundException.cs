namespace TodoApp.API.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException(Guid Id)
        : base($"Todo is not found for Id - {Id}")
    {
    }
}
