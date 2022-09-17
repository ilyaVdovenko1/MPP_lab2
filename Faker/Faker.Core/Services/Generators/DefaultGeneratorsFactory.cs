using Faker.Core.Interfaces;
using Faker.Core.Services.Generators.CollectionGenerators;
using Faker.Core.Services.Generators.ValueTypesGenerators;

namespace Faker.Core.Services.Generators;

public static class DefaultGeneratorsFactory
{
    public static IValueGenerator CreateDefaultGenerator()
    {
        var generator = new BoolGenerator();
        generator.SetNext(new CharGenerator());
        generator.SetNext(new DoubleGenerator());
        generator.SetNext(new IntegerGenerator());
        generator.SetNext(new DateTimeGenerator());
        generator.AddDefaultCollectionsGenerators();
        
        return generator;

    }
    
    public static IValueGenerator AddDefaultCollectionsGenerators(this IValueGenerator generator)
    {
        generator.SetNext(new StringGenerator());
        generator.SetNext(new ListGenerator());
        
        return generator;
    }
    
    public static IValueGenerator AddPrimitiveGenerators(this IValueGenerator generator)
    {
        generator.SetNext(new BoolGenerator());
        generator.SetNext(new CharGenerator());
        generator.SetNext(new DoubleGenerator());
        generator.SetNext(new IntegerGenerator());
        generator.SetNext(new DateTimeGenerator());

        return generator;
    }
    
}