using System.Linq.Expressions;
using System.Reflection;
using Faker.Core.Interfaces;
using Faker.Core.Services.Generators;

namespace Faker.Core.Services;

public class EmptyPicker : IPropertyPicker
{
    public void Add<ClassToPick, TypeToGenerate>(Expression<Func<ClassToPick, TypeToGenerate>> propertyPicker, IValueGenerator valueGenerator)
    {
    }

    public bool ValidateClass(Type type)
    {
        return true;
    }

    public IValueGenerator PropertyGenerator => new EmptyGenerator();
    public bool ShouldPickProperty(PropertyInfo propertyInfo)
    {
        return false;
    }

    public bool ShouldPickConstructorParam(ParameterInfo parameterInfo)
    {
        return false;
    }
}