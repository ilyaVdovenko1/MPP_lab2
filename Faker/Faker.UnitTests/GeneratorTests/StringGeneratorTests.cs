using System;
using System.Linq;
using Faker.Core.Exceptions;
using Faker.Core.Models;
using Faker.Core.Services.Generators.CollectionGenerators;
using Faker.Core.Services.Generators.ValueTypesGenerators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Faker.UnitTests.GeneratorTests;

[TestClass]
public class StringGeneratorTests
{
    private readonly Random rnd = new Random(12);
    
    
    
    [TestMethod]
    public void Generate_SeededRandom_GetSameValues()
    {
        //init
        var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789".ToList();
        
        var expectedLength = this.rnd.Next(0, 101);
        var expectedStr = new string(Enumerable.Repeat(alphabet, expectedLength)
            .Select(s => s[this.rnd.Next(alphabet.Count)]).ToArray());
        var generator = new StringGenerator();
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (string)generator.Generate(typeof(string), context);
        
        //assert
        Assert.AreEqual(expectedStr, actual);
    }
    
    [TestMethod]
    public void Generate_SeededRandomCustomAlphabetAndRange_GetSameValues()
    {
        //init
        var alphabet = "abcdefghijklmnopqrstuvwxyz".ToList();
        var range = new Range<int>(0, 10);
        var expectedLength = this.rnd.Next(0, 11);
        var expectedStr = new string(Enumerable.Repeat(alphabet, expectedLength)
            .Select(s => s[this.rnd.Next(alphabet.Count)]).ToArray());
        
        var generator = new StringGenerator(alphabet, range);
        var fakerMock = new Mock<Core.Services.Faker>(generator);
        var context = new GeneratorContext(new Random(12), fakerMock.Object);
        
        //act
        var actual = (string)generator.Generate(typeof(string), context);
        
        //assert
        Assert.AreEqual(expectedStr, actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(WrongTypeToGenerateException))]
    public void Generate_WrongType_GetWrongTypeException()
    {
        //init
        var generator = new StringGenerator();
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
        var generator = new StringGenerator();

        //act
        var actual = generator.CanGenerate(typeof(string));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void CanGenerate_WrongType_False()
    {
        //init
        var expected = false;
        var generator = new StringGenerator();

        //act
        var actual = generator.CanGenerate(typeof(bool));
        
        //assert
        Assert.AreEqual(expected, actual);
    }
    
}