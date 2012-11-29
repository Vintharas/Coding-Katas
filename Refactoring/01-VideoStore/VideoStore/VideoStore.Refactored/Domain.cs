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
 *            - We have effectively separated the statement formatting logic from the charge and frequent renter points calculations
 * 
 * 2) 
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
    public class Movie
    {
        public const int CHILDREN = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public string Title { get; set; }
        public int PriceCode { get; set; }
    }

    public class Rental
    {
        public Movie Movie { get; set; }
        public int DaysRented { get; set; }
        public double Charge { get { return CalculateCharge(); }}
        public int FrequentRenterPoints { get { return GetFrequentRenterPoints(); } }

        private double CalculateCharge()
        {   
            double cost = 0;
            switch (Movie.PriceCode)
            {
                case Movie.REGULAR:
                    cost += 2;
                    if (DaysRented > 2)
                        cost += (DaysRented - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    cost += DaysRented * 3;
                    break;
                case Movie.CHILDREN:
                    cost += 1.5;
                    if (DaysRented > 3)
                        cost += (DaysRented - 3) * 1.5;
                    break;
            }
            return cost;
        }

        private int GetFrequentRenterPoints()
        {
            if ((Movie.PriceCode == Movie.NEW_RELEASE) && DaysRented > 1)
                return 2;
            return 1;
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
