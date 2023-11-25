namespace Task_Manager.Configurations.Exceptions;
public class BadRequestException : Exception
{
    public BadRequestException(string msg) : base(msg)
    {

    }
}