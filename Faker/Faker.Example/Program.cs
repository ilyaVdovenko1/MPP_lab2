using System.Linq.Expressions;
using Faker.Core.Exceptions;
using Faker.Core.Services;
using Faker.Core.Services.Generators;
using Faker.Core.Services.Generators.UsersTypesGenerators.UserClassProcessor;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Faker.Example.TestClasses;
using Faker = Faker.Core.Services.Faker;

var generators = DefaultGeneratorsFactory.CreateDefaultGenerator();

var faker = new global::Faker.Core.Services.Faker(generators);

var boolValue = faker.Create<bool>();
var intValue = faker.Create<int>();
var charValue = faker.Create<char>();
var doubleValue = faker.Create<double>();
var user = faker.Create<User>();
var stringValue = faker.Create<string>();
var listValues = faker.Create<List<List<int>>>();


Console.WriteLine($"bool: {boolValue}");
Console.WriteLine($"int: {intValue}");
Console.WriteLine($"char: {charValue}");
Console.WriteLine($"double: {doubleValue}");
Console.WriteLine($"user name: {user.Name}, user phone {user.Phone.PhoneNumber}, user age: {user.Age}");
Console.WriteLine($"string: {stringValue}");
Console.WriteLine("number matrix:");
foreach (var values in listValues)
{
    foreach (var value in values)
    {
        Console.Write($"{value} ");
    }
    Console.WriteLine();
}

try
{
    faker.Create<WrongClassA>();

}
catch (CycledDependencyException)
{
    Console.WriteLine("Catched!");
}

var configuration = new DefaultPicker();
configuration.Add<CustomUser, int>(x => x.Id, new IntegerGenerator());

var customGenerator = new UserClassGenerator(configuration);
customGenerator.SetNext(new IntegerGenerator());

var customFaker = new global::Faker.Core.Services.Faker(customGenerator);
var customUser = customFaker.Create<CustomUser>();
Console.WriteLine($"Custom user. Age: {customUser.Age}, Id: {customUser.Id}");