using System.Reflection;

namespace Faker.Core.Services.Generators.UsersTypesGenerators.UserClassProcessor;

public class ConstructorComparer : IComparer<ConstructorInfo>
{
    public int Compare(ConstructorInfo? x, ConstructorInfo? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        
        return x.GetParameters().Length.CompareTo(y.GetParameters().Length);
    }
}