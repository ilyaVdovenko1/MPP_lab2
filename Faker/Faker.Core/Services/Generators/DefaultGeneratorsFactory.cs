using Faker.Core.Interfaces;
using Faker.Core.Services.Generators.CollectionGenerators;
using Faker.Core.Services.Generators.UsersTypesGenerators.UserClassProcessor;
using Faker.Core.Services.Generators.ValueTypesGenerators;

namespace Faker.Core.Services.Generators;

public static class DefaultGeneratorsFactory
{
    public static IValueGenerator CreateDefaultGenerator()
    {
        var boolGenerator = new BoolGenerator();
        var charGenerator = new CharGenerator();
        var doubleGenerator = new DoubleGenerator();
        var intGenerator = new IntegerGenerator();
        var dateTimeGenerator = new DateTimeGenerator();
        var stringGenerator = new StringGenerator();
        var listGenerator = new ListGenerator();
        var userClassGenerator = new UserClassGenerator();
        
        boolGenerator.SetNext(charGenerator);
        charGenerator.SetNext(doubleGenerator);
        doubleGenerator.SetNext(intGenerator);
        intGenerator.SetNext(dateTimeGenerator);
        dateTimeGenerator.SetNext(stringGenerator);
        stringGenerator.SetNext(listGenerator);
        listGenerator.SetNext(userClassGenerator);
        userClassGenerator.SetNext(boolGenerator);
        
        return boolGenerator;

    }

}