using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.ValueTypesGenerators;

public class DoubleGenerator : GeneratorBase
{
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            base.Generate(typeToGenerate, context);
        }
        
        return context.Random.NextDouble();
    }

    public override bool CanGenerate(Type type)
    {
        return type == typeof(double);
    }
}