using System.Collections;
using System.Reflection;
using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.CollectionGenerators;

public class ListGenerator : GeneratorBase
{
    private const int DefaultMinLength = 1;
    private const int DefaultMaxLength = 10;
    
    private readonly Range<int> lengthRange;

    public ListGenerator()
    {
        lengthRange = new Range<int>(DefaultMinLength, DefaultMaxLength);
    }
    
    public ListGenerator(Range<int> lengthRange)
    {
        this.lengthRange = lengthRange;
    }
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            return base.Generate(typeToGenerate, context);
        }
        
        var length = context.Random.Next(this.lengthRange.Minimum, this.lengthRange.Maximum + 1);
        
        var generationType = typeToGenerate.GetGenericArguments().FirstOrDefault() 
                             ?? throw new WrongTypeToGenerateException($"This generator can not generate this type: {typeToGenerate}");
        
        var list = (IList?)Activator.CreateInstance(typeToGenerate)
                   ?? throw new WrongTypeToGenerateException($"This generator can not generate this type: {typeToGenerate}");
        
        for (var i = 0; i < length - 1; i++)
        {
            list.Add(context.Faker.Create(generationType));
            Thread.Sleep(1);
        }

        return list;
    }

    public override bool CanGenerate(Type type)
    {
        return type.GetInterfaces().Contains(typeof(IList));
    }
}