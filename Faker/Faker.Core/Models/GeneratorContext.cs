using Faker.Core.Interfaces;

namespace Faker.Core.Models;

public class GeneratorContext
{
    public GeneratorContext(Random random, IFaker faker)
    {
        Random = random;
        Faker = faker;
    }
    
    public Random Random { get; }
    
    public IFaker Faker { get; }

}