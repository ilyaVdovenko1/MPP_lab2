using Faker.Core.Models;

namespace Faker.Core.Services.Generators;

public class EmptyGenerator : GeneratorBase
{
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        return null;
    }

    public override bool CanGenerate(Type type)
    {
        return false;
    }
}