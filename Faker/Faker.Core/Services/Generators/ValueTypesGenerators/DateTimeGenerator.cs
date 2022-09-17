using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.ValueTypesGenerators;

public class DateTimeGenerator : GeneratorBase
{
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            base.Generate(typeToGenerate, context);
        }
        
        var year = context.Random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year + 1);
        var month = context.Random.Next(1, 13);
        var day = context.Random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        var hour = context.Random.Next(0, 24);
        var minute = context.Random.Next(0, 60);
        var second = context.Random.Next(0, 60);
        var millisecond = context.Random.Next(0, 100);

        return new DateTime(year, month, day, hour, minute, second, millisecond);
    }

    public override bool CanGenerate(Type type)
    {
        return type == typeof(DateTime);
    }
}