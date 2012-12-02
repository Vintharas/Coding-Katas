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
                    Movie = new Movie { Title = "Cars", PriceCode = new ChildrenPrice()},
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
                    Movie = new Movie { Title = "Cars", PriceCode = new ChildrenPrice() },
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
                    Movie = new Movie { Title = "Cars", PriceCode = new ChildrenPrice() },
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
                    Movie = new Movie { Title = "Cars", PriceCode = new ChildrenPrice() },
                    DaysRented = 1
                });
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Cars 2", PriceCode = new ChildrenPrice() },
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
                    Movie = new Movie {Title = "Johnny Mnemonic", PriceCode = new RegularPrice()},
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
                    Movie = new Movie { Title = "Johnny Mnemonic", PriceCode = new RegularPrice() },
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
            // Arrange
            var customer = new Customer { Name = "John Doe" };
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Johnny Mnemonic", PriceCode = new RegularPrice() },
                    DaysRented = 1
                });
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Terminator 2", PriceCode = new RegularPrice() },
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tJohnny Mnemonic\t2$\n\tTerminator 2\t2$\nAmount owed is 4$.\nYou earned 2 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingANewReleaseMovieForADay_ShouldProduceAppropriateStatement()
        {
            // Arrange
            var customer = new Customer() {Name = "John Doe"};
            customer.AddRental(new Rental
                {
                    Movie = new Movie {  Title = "Skyfall", PriceCode = new NewReleasePrice()},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t3$\nAmount owed is 3$.\nYou earned 1 frequent renter points."));
        }

        [TestCase(arg1: 2, arg2: "6$")]
        [TestCase(arg1: 3, arg2: "9$")]
        [TestCase(arg1: 4, arg2: "12$")]
        public void Statement_WhenHavingANewReleaseMovieForMoreThanADay_ShouldPenalizeWithThreeDollarsPerDay(int daysRented, string amountOwed)
        {
            // Arrange
            var customer = new Customer() { Name = "John Doe" };
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Skyfall", PriceCode = new NewReleasePrice() },
                    DaysRented = daysRented
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo(string.Format("Rental Record for John Doe.\n\tSkyfall\t{0}\nAmount owed is {0}.\nYou earned 2 frequent renter points.", amountOwed)));
        }

        [Test]
        public void Statement_WhenHavingANewReleaseMovieForMoreThanADay_ShouldGetAnExtraFrequentRenterPoint()
        {
            // Arrange
            var customer = new Customer() { Name = "John Doe" };
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Skyfall", PriceCode = new NewReleasePrice() },
                    DaysRented = 2
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t6$\nAmount owed is 6$.\nYou earned 2 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingTwoNewReleaseMovies_ShouldGetTwoFrequentRenterPoints()
        {
            // Arrange
            var customer = new Customer() { Name = "John Doe" };
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Skyfall", PriceCode = new NewReleasePrice() },
                    DaysRented = 1
                });
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Prometheus", PriceCode = new NewReleasePrice() },
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t3$\n\tPrometheus\t3$\nAmount owed is 6$.\nYou earned 2 frequent renter points."));
        }

        [Test]
        public void Statement_WhenHavingMoviesFromDifferentCategories_ShouldGetAppropriateStatement()
        {
            // Arrange
            var customer = new Customer() { Name = "John Doe" };
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Skyfall", PriceCode = new NewReleasePrice() },
                    DaysRented = 1
                });
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "The Lord Of The Rings", PriceCode = new RegularPrice() },
                    DaysRented = 1
                });
            customer.AddRental(new Rental
                {
                    Movie = new Movie { Title = "Brave", PriceCode = new ChildrenPrice()},
                    DaysRented = 1
                });
            // Act
            string statement = customer.Statement();
            // Assert
            Assert.That(statement, Is.EqualTo("Rental Record for John Doe.\n\tSkyfall\t3$\n\tThe Lord Of The Rings\t2$\n\tBrave\t1.5$\nAmount owed is 6.5$.\nYou earned 3 frequent renter points."));
        }

        [Test]
        public void HtmlStatement_WhenHavingMoviesFromDifferentCategories_ShouldGetAppropriateStatement()
        {
            // Arrange
            var customer = new Customer() { Name = "John Doe" };
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Skyfall", PriceCode = new NewReleasePrice() },
                DaysRented = 1
            });
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "The Lord Of The Rings", PriceCode = new RegularPrice() },
                DaysRented = 1
            });
            customer.AddRental(new Rental
            {
                Movie = new Movie { Title = "Brave", PriceCode = new ChildrenPrice() },
                DaysRented = 1
            });
            // Act
            string statement = customer.HtmlStatement();
            // Assert
            Assert.That(statement, Is.EqualTo("<h1>Rental Record for <em>John Doe</em>.</h1>" + 
                                              "<p><ul><li>Skyfall : 3$</li><li>The Lord Of The Rings : 2$</li><li>Brave : 1.5$</li></ul></p>" + 
                                              "<p>Amount owed is 6.5$.</p><p>You earned 3 frequent renter points.</p>"));
        }

    }
}
