using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz
{
    public class Player
    {
        public string Name { get; set; }

        /// <summary>
        /// Answer depending on the number. If the number is divisible by 3 answer "Fizz", if it is divisible by 5 answer
        /// "Buzz", if it is both divisible by 3 and 5 answer "FizzBuzz", otherwise answer the same number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string Answer(int number)
        {
            if (number.IsDivisibleByThree() && number.IsDivisibleByFive())
                return "FizzBuzz";
            else if (number.IsDivisibleByThree())
                return "Fizz";
            else if (number.IsDivisibleByFive())
                return "Buzz";
            else
                return number.ToString();
        }

    }

    public static class ExtensionMethods
    {
        public static bool IsDivisibleByThree(this int number)
        {
            return number % 3 == 0;
        }

        public static bool IsDivisibleByFive(this int number)
        {
            return number % 5 == 0;
        }
    }
}
