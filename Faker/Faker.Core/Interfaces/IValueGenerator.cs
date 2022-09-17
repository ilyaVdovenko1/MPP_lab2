using Faker.Core.Models;

namespace Faker.Core.Interfaces;

public interface IValueGenerator
{
    public void SetNext(IValueGenerator generator);
    
    public object? Generate(Type typeToGenerate, GeneratorContext context);
    
    public bool CanGenerate(Type type);
}