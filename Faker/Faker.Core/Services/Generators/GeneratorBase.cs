using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators;

public abstract class GeneratorBase : IValueGenerator
{
    private IValueGenerator? nextGenerator;
    
    
    public void SetNext(IValueGenerator generator)
    {
        this.nextGenerator = generator;
    }

    public virtual object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (nextGenerator != null)
        {
            return this.nextGenerator.Generate(typeToGenerate, context);
        }

        throw new WrongTypeToGenerateException("No generators for this type.");
    }

    public abstract bool CanGenerate(Type type);
}