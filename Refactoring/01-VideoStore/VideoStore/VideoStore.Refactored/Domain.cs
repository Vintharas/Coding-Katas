using System;
using System.Collections.Generic;
using System.Linq;


/*
 * The problem: 
 * 
 * We have a program to calculate and print a statement of a customer's charges at a video store.
 * The program is told which movies a customer rented and for how long. It then calculates the charges, 
 * which depend on how long the movie is rented, and identifies the type movie. There are three kinds of 
 * movies: regular, children's, and new releases. In addition to calculating charges, the statement also
 * computes frequent renter points, which vary depending on whether the film is a new release.
 * 
 * And of course... new requirements come:
 * 
 *   - the customer wants a statement printed in HTML
 *   - the customer wants to change the system for classifying movies. These changes will affect:
 *       - the way renters are charged for movies and,
 *       - the way frequent renter points are calculated
 *      
 */

/*
 * Refactoring Summary:
 * 
 * 1) Need to add a new method HtmlStatement() to print a statement in HTML:
 *      a) Write tests to ensure that refactoring doesn't break functionality
 *      b) Refactor the Statement() method and extract the CalculateCharge() method that calculates a rental charge
 *      c) Move CalculateCharge() to the Rental class, and refactor it into readonly property Charge
 *      d) Refactor the Statement() method and extract the GetFrequentRenterPoints() method that returns the frequent renter points for a given rental
 *      e) Move the GetFrequentRenterPoints to the Rental class, and refactor it into a readonly property FrequenRenterPoints
 *      f) Extracted calculation of both total charge amount and frequent renter points to CalculateTotalChargeAmount() and CalculateFrequentRenterPoints() methods
 *      g) Write HtmlStatement() method taking advantage of refactored code
 * 
 * ##Conclusion 1## We have effectively separated the statement formatting logic from the charge and frequent renter points calculations
 * 
 * 
 * 2) Need to add new classifications, and the existing ones may be changed. Charges and frequent renter point calculation may change as well:
 *      a) Move CalculateCharge() and GetFrequentRenterPoints() to the Movie class
 *      b) Polymorphism to the rescue! Since a movie can change its classification over time, we use the State design pattern (GOF) 
 *         http://en.wikipedia.org/wiki/State_pattern instead of inheritance
 *          b1) Abstract prices of movies with Price states: ChildrenPrice, RegularPrice, NewReleasePrice
 *          b2) Substitute constant controlled pricing for state controlled pricing
 *      c) Modify Movie.PriceCode property to use the Price abstraction instead of the integer constant 
 *         (so we can avoid the factory method being tied to constants)
 *          
 * 
 * ##Conclusion 2## With this refactoring adding new classifications (ClassicPrice, AdultPrice, etc) and pricing and frequent renter
 * point strategies is as easy as implementing a new Price derived class (Open/Closed Principle http://en.wikipedia.org/wiki/Open/closed_principle)
 * 
 * 
 */

/*
 * 
 * Tip I: Refactoring changes the programs in small steps. If you make a mistake, it is easy to find the bug
 * Tip II: Any tool can write code that a computer can understand. Good programmers write code that humans can understand.
 *         Code that communicates its purpose is paramount.
 * 
 * 
 */

namespace VideoStore.Refactored
{

    public abstract class Price
    {
        public abstract double CalculateCharge(int daysRented);

        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
    }

    public class ChildrenPrice : Price
    {

        public override double CalculateCharge(int daysRented)
        {
            double charge = 1.5;
            if (daysRented > 3)
                charge += (daysRented - 3) * 1.5;
            return charge;
        }
    }

    public class RegularPrice : Price
    {

        public override double CalculateCharge(int daysRented)
        {
            double charge = 2;
            if (daysRented > 2)
                charge += (daysRented - 2) * 1.5;
            return charge;
        }

    }

    public class NewReleasePrice : Price
    {
        public override double CalculateCharge(int daysRented)
        {
            return daysRented * 3;
        }

        public override int GetFrequentRenterPoints(int daysRented)
        {
            return daysRented > 1 ? 2 : 1;
        }
    }


    public class Movie
    {
        public string Title { get; set; }
        public Price PriceCode { get; set; }

        public double CalculateCharge(int daysRented)
        {
            return PriceCode.CalculateCharge(daysRented);
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return PriceCode.GetFrequentRenterPoints(daysRented);
        }
    }



    public class Rental
    {
        public Movie Movie { get; set; }
        public int DaysRented { get; set; }
        public double Charge { get { return CalculateCharge(); }}
        public int FrequentRenterPoints { get { return GetFrequentRenterPoints(); } }

        private double CalculateCharge()
        {
            return Movie.CalculateCharge(DaysRented);
        }

        private int GetFrequentRenterPoints()
        {
            return Movie.GetFrequentRenterPoints(DaysRented);
        }

    }

    public class Customer
    {
        public string Name { get; set; }
        private readonly List<Rental> rentals = new List<Rental>(); 

        public void AddRental(Rental rental)
        {
            rentals.Add(rental);
        }

        public String Statement()
        {
            double totalAmount = CalculateTotalChargeAmount();
            double frequentRenterPoints = CalculateFrequentRenterPoints();
            return FormatPrintStatement(totalAmount, frequentRenterPoints);
        }

        private string FormatPrintStatement(double totalAmount, double frequentRenterPoints)
        {
            // Header
            String result = "Rental Record for " + Name + ".\n";
            // Body
            foreach (var rental in rentals)
                result += "\t" + rental.Movie.Title + "\t" + rental.Charge + "$\n";
            // Footer
            result += "Amount owed is " + totalAmount + "$.\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points.";
            return result;
        }

        public String HtmlStatement()
        {
            double totalAmount = CalculateTotalChargeAmount();
            double frequentRenterPoints = CalculateFrequentRenterPoints();
            return FormatHtmlStatement(totalAmount, frequentRenterPoints);
        }

        private string FormatHtmlStatement(double totalAmount, double frequentRenterPoints)
        {
            // Header
            String result = "<h1>Rental Record for <em>" + Name + "</em>.</h1>";
            // Body
            result += "<p><ul>";
            foreach (var rental in rentals)
                result += "<li>" + rental.Movie.Title + " : " + rental.Charge + "$</li>";
            result += "</ul></p>";
            // Footer
            result += "<p>Amount owed is " + totalAmount + "$.</p>";
            result += "<p>You earned " + frequentRenterPoints + " frequent renter points.</p>";
            return result;
        }

        public double CalculateTotalChargeAmount()
        {
            return rentals.Sum(r => r.Charge);
        }

        public int CalculateFrequentRenterPoints()
        {
            return rentals.Sum(r => r.FrequentRenterPoints);
        }


    }




}
