namespace polyclinic_service.System.Exceptions;

public class WrongPassword : Exception
{
    public WrongPassword(string? message) : base(message)
    {
    }
}