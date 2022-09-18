using System;
using System.Collections.Generic;
using System.Linq;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.CollectionGenerators;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class ListGeneratorUnitTests
{
    private readonly Random rnd = new Random(12);
    
    [TestMethod]
    public void Generate_SeededRandom_GetSameValues()
    {
        //init
        var expectedLength = this.rnd.Next(1, 11);
        var expected = new List<int>();
        for (var i = 0; i < expectedLength; i++)
        {
            expected.Add(1);
        }

        var generator = new ListGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        fakerMock.Setup(x => x.Create(typeof(int))).Returns(1);
        
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (List<int>)generator.Generate(typeof(List<int>), context);
        
        //assert
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void Generate_SeededRandomCustomRange_GetSameValues()
    {
        //init
        var range = new Range<int>(1, 20);
        var expectedLength = this.rnd.Next(1, 21);
        var expected = new List<int>();
        
        for (var i = 0; i < expectedLength; i++)
        {
            expected.Add(1);
        }

        var generator = new ListGenerator(range);
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        fakerMock.Setup(x => x.Create(typeof(int))).Returns(1);
        
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (List<int>)generator.Generate(typeof(List<int>), context);
        
        //assert
        CollectionAssert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new ListGenerator();
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
        var generator = new ListGenerator();
        

        //act
        var actual = generator.CanGenerate(typeof(List<bool>));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new ListGenerator();
        

        //act
        var actual = generator.CanGenerate(typeof(bool));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
}