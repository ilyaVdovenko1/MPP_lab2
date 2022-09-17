namespace Faker.Core.Exceptions;

public class CycledDependencyException : Exception
{
    public CycledDependencyException()
    {
    }

    public CycledDependencyException(string message)
        : base(message)
    {
    }

    public CycledDependencyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}