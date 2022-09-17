namespace Faker.Core.Interfaces;

public interface IFaker
{
    public T? Create<T>();

    public object? Create(Type type);
}