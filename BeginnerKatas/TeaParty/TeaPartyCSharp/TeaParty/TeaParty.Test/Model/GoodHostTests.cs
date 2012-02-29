using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeaParty.Model;

namespace TeaParty.Test.Model
{
    [TestClass]
    public class GoodHostTests
    {


        [TestMethod]
        public void Welcome_WhenWelcomingAFemaleCommoner_ShouldSayHelloMsLastName()
        {
            // Arrange
            GoodHost host = new GoodHost();
            Guest femaleCommoner = new Guest {LastName = "Williams", Gender = Gender.Female, Status = Status.Commoner};
            // Act
            var greeting = host.Welcome(femaleCommoner);
            // Assert
            Assert.AreEqual(expected: "Hello Ms. Williams", actual: greeting);
        }

        [TestMethod]
        public void Welcome_WhenWelcomingAFemaleNoble_ShouldSayHelloLadyLastName()
        {
            // Arrange
            GoodHost host = new GoodHost();
            Guest femaleNoble = new Guest {LastName = "Fitzpatrick", Gender = Gender.Female, Status = Status.Noble};
            // Act
            var greeting = host.Welcome(femaleNoble);
            // Assert
            Assert.AreEqual(expected: "Hello Lady Fitzpatrick", actual: greeting);
        }

        [TestMethod]
        public void Welcome_WhenWelcomingAMaleCommoner_ShouldSayHelloMrLastName()
        {
            // Arrange
            GoodHost host = new GoodHost();
            Guest maleCommoner = new Guest { LastName = "Doe", Gender = Gender.Male, Status = Status.Commoner};
            // Act
            var greeting = host.Welcome(maleCommoner);
            // Assert
            Assert.AreEqual(expected: "Hello Mr. Doe", actual: greeting);
        }

        [TestMethod]
        public void Welcome_WhenWelcomingAMaleNoble_ShouldSayHelloSirLastName()
        {
            // Arrange
            GoodHost host = new GoodHost();
            Guest maleCommoner = new Guest { LastName = "Lancelot", Gender = Gender.Male, Status = Status.Noble };
            // Act
            var greeting = host.Welcome(maleCommoner);
            // Assert
            Assert.AreEqual(expected: "Hello Sir Lancelot", actual: greeting);
        }

    }
}
