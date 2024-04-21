namespace TodoApp.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Guid Id)
        : base($"Todo is not found for Id - {{Id}}")
    {
    }
}
