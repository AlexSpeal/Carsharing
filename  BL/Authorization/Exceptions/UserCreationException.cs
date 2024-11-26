namespace BL.Authorization.Exceptions;

public class UserCreationException: ApplicationException
{
    public UserCreationException() { }

    public UserCreationException(string message) : base(message) { }
}