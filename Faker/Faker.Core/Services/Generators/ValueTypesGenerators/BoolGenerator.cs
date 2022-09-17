using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.ValueTypesGenerators;

public class BoolGenerator : GeneratorBase
{
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            base.Generate(typeToGenerate, context);
        }

        return context.Random.Next() % 2 == 0;
    }

    public override bool CanGenerate(Type type)
    {
        return type == typeof(bool);
    }
}