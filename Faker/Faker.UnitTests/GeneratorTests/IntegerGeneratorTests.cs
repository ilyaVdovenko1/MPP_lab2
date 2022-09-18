using System;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class IntegerGeneratorTests
{
    private readonly Random rnd = new Random(12);
    
    
    
    [TestMethod]
    public void Generate_SeededRandom_GetSameValues()
    {
        //init
        var expected = rnd.Next();
        var generator = new IntegerGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (int)generator.Generate(typeof(int), context);
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new IntegerGenerator();
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
        var generator = new IntegerGenerator();

        //act
        var actual = generator.CanGenerate(typeof(int));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new IntegerGenerator();

        //act
        var actual = generator.CanGenerate(typeof(bool));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
}