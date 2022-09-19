using System.Linq.Expressions;
using System.Reflection;

namespace Faker.Core.Interfaces;

public interface IPropertyPicker
{
    public void Add<ClassToPick, TypeToGenerate>(Expression<Func<ClassToPick, TypeToGenerate>> propertyPicker, IValueGenerator valueGenerator);
    public bool ValidateClass(Type type);

    public IValueGenerator PropertyGenerator { get; }

    public bool ShouldPickProperty(PropertyInfo propertyInfo);

    public bool ShouldPickConstructorParam(ParameterInfo parameterInfo);
}