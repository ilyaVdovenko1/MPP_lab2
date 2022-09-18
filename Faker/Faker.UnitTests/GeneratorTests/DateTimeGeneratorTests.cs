using System;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class DateTimeGeneratorTests
{
    private readonly Random rnd = new Random(12);
    
    
    
    [TestMethod]
    public void Generate_SeededRandom_GetSameValues()
    {
        //init
        var year = rnd.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year + 1);
        var month = rnd.Next(1, 13);
        var day = rnd.Next(1, DateTime.DaysInMonth(year, month) + 1);
        var hour = rnd.Next(0, 24);
        var minute = rnd.Next(0, 60);
        var second = rnd.Next(0, 60);
        var millisecond = rnd.Next(0, 100);
        var expected = new DateTime(year, month, day, hour, minute, second, millisecond);
        
        
        var generator = new DateTimeGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (DateTime)generator.Generate(typeof(DateTime), context);
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new DateTimeGenerator();
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
        var generator = new DateTimeGenerator();

        //act
        var actual = generator.CanGenerate(typeof(DateTime));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new DateTimeGenerator();

        //act
        var actual = generator.CanGenerate(typeof(object));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
}