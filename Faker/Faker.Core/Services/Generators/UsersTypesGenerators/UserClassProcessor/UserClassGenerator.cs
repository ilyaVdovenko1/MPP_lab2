using System.Reflection;
using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.UsersTypesGenerators.UserClassProcessor;

public class UserClassGenerator : GeneratorBase
{
    private readonly IPropertyPicker picker;
    private readonly HashSet<Type> types;

    public UserClassGenerator(IPropertyPicker picker)
    {
        this.picker = picker;
        this.types = new HashSet<Type>();
    }

    public UserClassGenerator()
    {
        this.picker = new EmptyPicker();
        this.types = new HashSet<Type>();
    }

    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!this.picker.ValidateClass(typeToGenerate))
        {
            return base.Generate(typeToGenerate, context);
        }
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
        
        var customPropValues = properties.Where(propertyInfo => propertyInfo.CanWrite && this.picker.ShouldPickProperty(propertyInfo)).ToDictionary(propertyInfo => propertyInfo, propertyInfo => base.GenerateWithConcreteGenerator(this.picker.PropertyGenerator, propertyInfo.PropertyType, context));
        var propValues = properties.Where(propertyInfo => propertyInfo.CanWrite && !this.picker.ShouldPickProperty(propertyInfo)).ToDictionary(propertyInfo => propertyInfo, propertyInfo => context.Faker.Create(propertyInfo.PropertyType));

        var constructors = typeToGenerate.GetConstructors();
        var comparer = new ConstructorComparer();
        Array.Sort(constructors, comparer);
        object? obj = null;

        foreach (var constructor in constructors)
        {
            var paramsTypes = constructor.GetParameters();


            try
            {
                obj = constructor.Invoke(paramsTypes.Select(type => this.picker.ShouldPickConstructorParam(type)
                    ? this.picker.PropertyGenerator.Generate(type.ParameterType, context)
                    : context.Faker.Create(type.ParameterType)).ToArray());
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

        foreach (var propValue in customPropValues)
        {
            propValue.Key.SetValue(obj, propValue.Value);
        }

        return obj;


    }

    public override bool CanGenerate(Type type)
    {
        return !type.IsAbstract && type.IsClass;
    }
}