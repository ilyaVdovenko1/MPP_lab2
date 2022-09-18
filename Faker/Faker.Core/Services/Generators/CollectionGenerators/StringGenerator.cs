using Faker.Core.Exceptions;
using Faker.Core.Interfaces;
using Faker.Core.Models;

namespace Faker.Core.Services.Generators.CollectionGenerators;

public class StringGenerator : GeneratorBase
{
    private const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const int DefaultMinLength = 0;
    private const int DefaultMaxLength = 100;
    
    private readonly IList<char> alphabet;
    private readonly Range<int> lengthRange;

    public StringGenerator()
    {
        this.alphabet = DefaultAlphabet.ToCharArray();
        this.lengthRange = new Range<int>(DefaultMinLength, DefaultMaxLength);
    }
    
    public StringGenerator(IList<char> alphabet, Range<int> lengthRange)
    {
        this.lengthRange = lengthRange;
        this.alphabet = alphabet;
    }
    public override object? Generate(Type typeToGenerate, GeneratorContext context)
    {
        if (!CanGenerate(typeToGenerate))
        {
            return base.Generate(typeToGenerate, context);
        }
        
        var length = context.Random.Next(this.lengthRange.Minimum, this.lengthRange.Maximum + 1);
        
        return new string(Enumerable.Repeat(this.alphabet, length)
            .Select(s => s[context.Random.Next(this.alphabet.Count)]).ToArray());
    }

    public override bool CanGenerate(Type type)
    {
        return type == typeof(string);
    }
}