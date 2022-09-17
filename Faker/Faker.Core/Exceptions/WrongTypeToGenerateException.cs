namespace Faker.Core.Exceptions;

public class WrongTypeToGenerateException : Exception
{
    public WrongTypeToGenerateException()
    {
    }

    public WrongTypeToGenerateException(string message)
        : base(message)
    {
    }

    public WrongTypeToGenerateException(string message, Exception inner)
        : base(message, inner)
    {
    }
}