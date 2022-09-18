using System.Reflection;
using Faker.Core.Exceptions;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.UsersTypesGenerators.UserClassProcessor;

public class UserClassGenerator : GeneratorBase
{
    private HashSet<Type> types;
    public UserClassGenerator()
    {
        this.types = new HashSet<Type>();
    }

    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            return base.Generate(typeToGenerate, context);
        }

        if (this.types.Contains(typeToGenerate))
        {
            throw new CycledDependencyException();
        }

        this.types.Add(typeToGenerate);

        var properties = typeToGenerate.GetProperties();
        var propValues = properties.Where(propertyInfo => propertyInfo.CanWrite).ToDictionary(propertyInfo => propertyInfo, propertyInfo => context.Faker.Create(propertyInfo.PropertyType));

        var constructors = typeToGenerate.GetConstructors();
        var comparer = new ConstructorComparer();
        Array.Sort(constructors, comparer);
        object? obj = null;

        foreach (var constructor in constructors)
        {
            var paramsTypes = constructor.GetParameters();

            try
            {
                obj = constructor.Invoke(paramsTypes.Select(info => context.Faker.Create(info.ParameterType)).ToArray());
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        foreach (var propWithSetter in propValues)
        {
            propWithSetter.Key.SetValue(obj, propWithSetter.Value);
        }

        return obj;


    }

    public override bool CanGenerate(Type type)
    {
        return !type.IsAbstract && type.IsClass;
    }
}