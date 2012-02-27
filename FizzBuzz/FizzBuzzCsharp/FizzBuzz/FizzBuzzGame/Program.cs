using System;
using FizzBuzz;

namespace FizzBuzzGame
{
    //  Any number divisible by three is replaced by the word fizz and any divisible by five by the word buzz. Numbers divisible by both become fizzbuzz. 
    //  Start with a random number. An example could be:
    //      2, fizz, 4, buzz, fizz, 7, 8, fizz, buzz, 11, fizz, 13, 14, fizzbuzz, etc...
    class Program
    {
        private const int MAX_NUMBER_WHEN_THE_GAME_GOT_TO_END = 999;

        static void Main(string[] args)
        {
            var player1 = new Player {Name = "John Doe"};
            var player2 = new Player {Name = "Jane Doe"};

            // Let the games begin!
            for (int i = AnyRandomInteger(); i < MAX_NUMBER_WHEN_THE_GAME_GOT_TO_END; i++)
            {
                if (i.IsEven())
                    Console.WriteLine("{0} says {1}", player2.Name, player2.Answer(i));
                else
                    Console.WriteLine("{0} says {1}", player1.Name, player1.Answer(i));
            }
            Console.WriteLine("THE END!! It was a DRAW! :)");
        }

        private static int AnyRandomInteger()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(MAX_NUMBER_WHEN_THE_GAME_GOT_TO_END);
        }
    }

    public static class GameExtensionMethods
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
    }
}
