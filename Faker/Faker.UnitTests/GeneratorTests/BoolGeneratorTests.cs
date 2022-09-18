using System;
using System.Reflection;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class BoolGeneratorTests
{
    private readonly Random rnd = new Random(12);
    
    
    
    [TestMethod]
    public void Generate_SeededRando_GetSameValues()
    {
        //init
        var expected = rnd.Next() % 2 == 0;
        var generator = new BoolGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (bool)generator.Generate(typeof(bool), context);
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new BoolGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        generator.Generate(typeof(int), context);
    }
    
    [TestMethod]
    public void CanGenerate_CorrectType_True()
    {
        //init
        var expected = true;
        var generator = new BoolGenerator();

        //act
        var actual = generator.CanGenerate(typeof(bool));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new BoolGenerator();

        //act
        var actual = generator.CanGenerate(typeof(int));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
}