using NUnit.Framework;

namespace VideoStore.Refactored.UnitTests
{
    [TestFixture]
    class CustomerTests
    {

        [Test]
        public void Statement_WhenHavingAChildrenMovieForADay_ShouldProduceAppropriateStatement()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe"};
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Cars", PriceCode = Movie.CHILDREN},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tCars\t1.5\nAmount owed is 1.5.\nYou earned 1 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingANormalMovieForADay_ShouldProduceAppropriateStatement()
        {
            // Arrange
            var customer = new Customer {Name = "John Doe"};
            customer.AddRental(new Rental
                {
                    Movie = new Movie {Title = "Johnny Mnemonic", PriceCode = Movie.REGULAR},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tJohnny Mnemonic\t2\nAmount owed is 2.\nYou earned 1 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingANewReleaseMovieForADay_ShouldProduceAppropriateStatement()
        {
            // Arrange
            var customer = new Customer() {Name = "John Doe"};
            customer.AddRental(new Rental
                {
                    Movie = new Movie {  Title = "Skyfall", PriceCode = Movie.NEW_RELEASE},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t3\nAmount owed is 3.\nYou earned 1 frequent renter points."));
        }

    }
}
