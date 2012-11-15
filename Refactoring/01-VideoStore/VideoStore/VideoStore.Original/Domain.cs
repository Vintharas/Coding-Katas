using System;
using System.Collections.Generic;


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

namespace VideoStore.Original
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
    }

    public class Customer
    {
        public string Name { get; set; }
        private readonly List<Rental> rentals = new List<Rental>(); 

        public void AddRental(Rental arg)
        {
            rentals.Add(arg);
        }

        public String statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            IEnumerator<Rental> rentalEnumerator = this.rentals.GetEnumerator();
            String result = "Rental Record for " + Name + "\n";
            while (rentalEnumerator.MoveNext())
            {
                double thisAmount = 0;
                Rental each = rentalEnumerator.Current;
                //determine amounts for each line
                switch (each.Movie.PriceCode)
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.DaysRented > 2)
                            thisAmount += (each.DaysRented - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.DaysRented * 3;
                        break;
                    case Movie.CHILDREN:
                        thisAmount += 1.5;
                        if (each.DaysRented > 3)
                            thisAmount += (each.DaysRented - 3) * 1.5;
                        break;
                }
                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if ((each.Movie.PriceCode == Movie.NEW_RELEASE) && each.DaysRented > 1) frequentRenterPoints++;
                //show figures for this rental
                    result += "\t" + each.Movie.Title + "\t" + thisAmount + "\n"; totalAmount += thisAmount;
             }
            //add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";
            return result;
        }

    }




}
