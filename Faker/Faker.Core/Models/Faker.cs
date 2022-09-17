using Faker.Core.Interfaces;

namespace Faker.Core.Models;

public class Faker : IFaker
{
    private readonly IValueGenerator generator;
    private readonly Random random;
    
    public Faker(IValueGenerator generator)
    {
        this.generator = generator;
        this.random = new Random();
    }
    
    public T? Create<T>()
    {
        return (T?)Create(typeof(T));
    }

    public object? Create(Type type)
    {
        var generatorContext = new GeneratorContext(this.random, this);

        return this.generator.Generate(type, generatorContext);
    }

    private static object? GetDefaultValue(Type type)
    {
        if (type.IsValueType)
            return Activator.CreateInstance(type);
        
        return null;
    }
}