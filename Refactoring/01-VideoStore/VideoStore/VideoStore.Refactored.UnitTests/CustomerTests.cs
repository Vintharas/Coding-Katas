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
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tCars\t1.5$\nAmount owed is 1.5$.\nYou earned 1 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingAChildrenMovieForTwoDays_ShouldProduceAppropriateStatement()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Cars", PriceCode = Movie.CHILDREN },
                DaysRented = 2
            });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tCars\t1.5$\nAmount owed is 1.5$.\nYou earned 1 frequent renter points."));
        }

        [TestCase(arg1: 4, arg2: "3$")]
        [TestCase(arg1: 5, arg2: "4.5$")]
        [TestCase(arg1: 6, arg2: "6$")]
        public void Statement_WhenHavingAChildrenMovieForMoreThanThreeDays_ShouldPenalizeWithOneAndAHalfDollarsPerExtraDay(int daysRented, string amountOwed)
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Cars", PriceCode = Movie.CHILDREN },
                DaysRented = daysRented
            });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo(string.Format("Rental Record for John Doe.\n\tCars\t{0}\nAmount owed is {0}.\nYou earned 1 frequent renter points.", amountOwed)));
        }

        [Test]
        public void Statement_WhenHavingTwoChildrenMovies_ShouldGetTwoFrequentRenterPoints()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Cars", PriceCode = Movie.CHILDREN },
                DaysRented = 1
            });
            customer.AddRental(new Rental
                {
                    Movie = new Movie {Title = "Cars 2", PriceCode = Movie.CHILDREN},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tCars\t1.5$\n\t" +
                                              "Cars 2\t1.5$\nAmount owed is 3$.\nYou earned 2 frequent renter points."));
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
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tJohnny Mnemonic\t2$\nAmount owed is 2$.\nYou earned 1 frequent renter points."));
        }

        [TestCase(arg1: 3, arg2: "3.5$")]
        [TestCase(arg1: 4, arg2: "5$")]
        [TestCase(arg1: 5, arg2: "6.5$")]
        public void Statement_WhenHavingANormalMovieMoreThanTwoDays_ShouldPenalizeWithOneAndAnExtraDollarPerExtraDay(int daysRented, string amountOwed)
        {
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Johnny Mnemonic", PriceCode = Movie.REGULAR },
                DaysRented = daysRented
            });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo(string.Format("Rental Record for John Doe.\n\tJohnny Mnemonic\t{0}\nAmount owed is {0}.\nYou earned 1 frequent renter points.", amountOwed)));
        }

        [Test]
        public void Statement_WhenHavingTwoNormalMovies_ShouldGetTwoFrequentRenterPoints()
        {
            
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
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t3$\nAmount owed is 3$.\nYou earned 1 frequent renter points."));
        }

    }
}
