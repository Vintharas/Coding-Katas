using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculatorKata.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {

        [Test]
        public void Add_WhenGivenAnEmptyString_ShouldReturnZero()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "");
            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Add_WhenGivenAStringWithASingleArgument_ShouldReturnIt()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "1");
            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [TestCase(arg1: "1", arg2: 1)]
        [TestCase(arg1: "2", arg2: 2)]
        [TestCase(arg1: "3", arg2: 3)]
        [TestCase(arg1: "10", arg2: 10)]
        public void Add_WhenGivenAStringWithASingleArgument_ShouldReturnIt2(string numbers, int expectedResult)
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers);
            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Add_WhenGivenAStringWithASingleArgumentThatIsNotANumber_ShouldThrowAnException()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act, Assert
            int result = calculator.Add(numbers: "x");
        }

        [Test]
        public void Add_WhenGivenAStringWithTwoArgumentsSeparatedByAComma_ShouldPerformAdditionOfThem()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "1,2");
            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_WhenGivenAStringWithThreeArgumentsSeparatedByAComma_ShouldPerformAdditionOfThem()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "1,1,1");
            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Add_WhenGivenAStringWithFourArgumentsSeparatedByAComma_ShouldPerformAdditionOfThem()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "1,1,1,1");
            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Add_WhenGivenAStringWithAnUndefinedNumberOfArgumentsSeparatedByACommna_ShouldPerformAdditionOfThem()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "2,2,2,2,2,2,2,2,2,2"); // these are ten 2's
            // Assert
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void Add_WhenGivenAStringWithArgumentsSeparatedByNewLines_ShouldPerformAddition()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "2\n3\n1");
            // Assert
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Add_WhenGivenAStringWithArgumentsSeparatedByNewLinesAndCommas_ShouldPerformAdditionOfThem()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "2\n3,1");
            // Assert
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void Add_WhenGivenAStringWithArgumentsAndTwoDelimitersConsecutively_ShouldThrowAnException()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act, Assert
            int result = calculator.Add(numbers: "1,\n");
        }

        [Test]
        [ExpectedException(typeof (FormatException))]
        public void Add_WhenGivenAStringWithSeveralArgumentsAndTwoDelimitersConsecutively_ShouldThrowAnException()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act, Assert
            int result = calculator.Add(numbers: "1,\n2,3,4");
        }

        [Test]
        public void Add_WhenGivenADelimiterAtTheBeginningOfTheStringOfNumber_ShouldEnableUsingThisDelimiter()
        {
            // Arrange
            var delimiterContainer = "//[{0}]\n";
            var delimiter = ";";
            var delimiterPrefix = string.Format(delimiterContainer, delimiter);
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: delimiterPrefix + "2;3");
            // Assert
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Add_WhenGivenADelimiterAtTheBeginningOfTheStringOfNumber_ShouldEnableUsingThisOtherDelimiter()
        {
            // Arrange
            var delimiterContainer = "//[{0}]\n";
            var delimiter = "***";
            var delimiterPrefix = string.Format(delimiterContainer, delimiter);
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: delimiterPrefix + "2***3***2");
            // Assert
            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        [ExpectedException(typeof(NegativeNumbersNotAllowedException), ExpectedMessage = "Negative numbers not allowed: -5")]
        public void Add_WhenAddingNumbersAndOneOfThemIsNegative_ShouldThrowANegativeNumbersNotAllowedExceptionAndShowTheNegativeNumberInTheExceptionMessage()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act, Assert
            int result = calculator.Add(numbers: "1,2,-5");
        }

        [Test]
        [ExpectedException(typeof(NegativeNumbersNotAllowedException), ExpectedMessage = "Negative numbers not allowed: -5,-1,-2,-3")]
        public void Add_WhenAddingNumbersAndSeveralOfThemAreNegative_ShouldThrowANegativeNumbersNotAllowedExceptionAndShowTheNegativeNumbersInTheExceptionMessage()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act, Assert
            int result = calculator.Add(numbers: "1,2,-5,-1,-2,-3");
        }

        [Test]
        public void Add_WhenAddingNumbersBiggerThanAThousand_ShouldIgnoreTheseNumbers()
        {
            // Arrange
            StringCalculator calculator = GetCalculator();
            // Act
            int result = calculator.Add(numbers: "1,2,1000");
            // Assert
            Assert.That(result, Is.EqualTo(3));
        }





        private StringCalculator GetCalculator()
        {
            return new StringCalculator();
        }
    }
}
