using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.ValueTypesGenerators;

public class CharGenerator : GeneratorBase
{
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            base.Generate(typeToGenerate, context);
        }
        
        return (char)context.Random.Next('A', 'Z');
    }

    public override bool CanGenerate(Type type)
    {
        return type == typeof(char);
    }
}