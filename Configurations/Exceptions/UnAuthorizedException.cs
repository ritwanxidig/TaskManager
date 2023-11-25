namespace Task_Manager.Configurations.Exceptions;
public class UnAuthorizedException : Exception
{
    public UnAuthorizedException(string msg) : base(msg)
    {

    }
}