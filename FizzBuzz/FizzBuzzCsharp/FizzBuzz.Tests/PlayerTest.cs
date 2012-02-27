using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Tests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Answer_WhenGivenANumberDivisibleBy3_ShouldReturnFizz()
        {
            // Arrange
            var player = new Player();
            var NUMBER_DIVISIBLE_BY_THREE = 3;
            // Act
            var playerAnswer = player.Answer(NUMBER_DIVISIBLE_BY_THREE);
            // Assert 
            Assert.AreEqual(expected: "Fizz", actual: playerAnswer);
        }

        [TestMethod]
        public void Answer_WhenGivenANumberDivisibleBy5_ShouldReturnBuzz()
        {
            // Arrange
            var player = new Player();
            var NUMBER_DIVISIBLE_BY_FIVE = 5;
            // Act
            var playerAnswer = player.Answer(NUMBER_DIVISIBLE_BY_FIVE);
            // Assert
            Assert.AreEqual(expected: "Buzz", actual: playerAnswer);
        }

        [TestMethod]
        public void Answer_WhenGivenANumberDivisibleBothBy3And5_ShouldReturnFizzBuzz()
        {
            // Arrange
            var player = new Player();
            var NUMBER_DIVISIBLE_BY_THREE_AND_FIVE = 15;
            // Act
            var playerAnswer = player.Answer(NUMBER_DIVISIBLE_BY_THREE_AND_FIVE);
            // Assert
            Assert.AreEqual(expected: "FizzBuzz", actual: playerAnswer);
        }

        [TestMethod]
        public void Answer_WhenGivenANumberNonDivisibleBy3Nor5_ShouldReturnTheSameNumber()
        {
            // Arrange
            var player = new Player();
            var NUMBER_NON_DIVISIBLE_BY_THREE_NOR_FIVE = 2;
            // Act
            var playerAnswer = player.Answer(NUMBER_NON_DIVISIBLE_BY_THREE_NOR_FIVE);
            // Assert
            Assert.AreEqual(expected: "2", actual: playerAnswer);
        }
    }
}
