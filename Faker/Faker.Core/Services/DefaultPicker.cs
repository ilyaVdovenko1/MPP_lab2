using System.Linq.Expressions;
using System.Reflection;
using Faker.Core.Interfaces;
using Faker.Core.Models;
using Faker.Core.Services.Generators;

namespace Faker.Core.Services;

public class DefaultPicker : IPropertyPicker
{
    private IValueGenerator? generator;
    private readonly Dictionary<Type, HashSet<PropertyInfo>> classesToPick;

    public DefaultPicker()
    {
        classesToPick = new Dictionary<Type, HashSet<PropertyInfo>>();
    }

    public void Add<ClassToPick, TypeToGenerate>(Expression<Func<ClassToPick, TypeToGenerate>> propertyPicker, IValueGenerator valueGenerator)
    {
        
        var type = typeof(ClassToPick);

        if (propertyPicker.Body is not MemberExpression member)
            throw new ArgumentException($"Expression '{propertyPicker.ToString()}' refers to a method, not a property.");

        var propInfo = member.Member as PropertyInfo;
        if (propInfo == null)
            throw new ArgumentException($"Expression '{propertyPicker.ToString()}' refers to a field, not a property.");

        if (propInfo.ReflectedType == null || type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            throw new ArgumentException(
                $"Expression '{propertyPicker.ToString()}' refers to a property that is not from type {type}.");

        if (!this.classesToPick.ContainsKey(typeof(ClassToPick)))
        {
            this.classesToPick[typeof(ClassToPick)] = new HashSet<PropertyInfo>();
        }
        
        this.generator?.SetNext(valueGenerator);
        this.generator = valueGenerator;

        
        this.classesToPick[typeof(ClassToPick)].Add(propInfo);
    }

    public bool ValidateClass(Type type)
    {
        return this.classesToPick.ContainsKey(type);
    }

    public IValueGenerator PropertyGenerator => generator ?? new EmptyGenerator();
    public bool ShouldPickProperty(PropertyInfo propertyInfo)
    {
        if (propertyInfo.DeclaringType is null)
        {
            return false;
        }

        return this.classesToPick.ContainsKey(propertyInfo.DeclaringType) &&
               this.classesToPick[propertyInfo.DeclaringType].Contains(propertyInfo);
            
    }

    public bool ShouldPickConstructorParam(ParameterInfo parameterInfo)
    {
        if (parameterInfo.Member.DeclaringType is null)
        {
            return false;
        }

        return this.classesToPick.ContainsKey(parameterInfo.Member.DeclaringType) && this
            .classesToPick[parameterInfo.Member.DeclaringType].Any(x =>
                string.Equals(x.Name, parameterInfo.Name, StringComparison.InvariantCultureIgnoreCase));
    }
}