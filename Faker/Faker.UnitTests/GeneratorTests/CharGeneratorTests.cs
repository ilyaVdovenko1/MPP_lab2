using System;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class CharGeneratorTests
{
    private readonly Random rnd = new Random(12);
    
    [TestMethod]
    public void Generate_SeededRandom_GetSameValues()
    {
        //init
        var expected = (char)rnd.Next('A', 'Z');
        var generator = new CharGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (char)generator.Generate(typeof(char), context);
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new CharGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        generator.Generate(typeof(bool), context);
    }
    
    [TestMethod]
    public void CanGenerate_CorrectType_True()
    {
        //init
        var expected = true;
        var generator = new CharGenerator();

        //act
        var actual = generator.CanGenerate(typeof(char));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new CharGenerator();

        //act
        var actual = generator.CanGenerate(typeof(bool));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
}