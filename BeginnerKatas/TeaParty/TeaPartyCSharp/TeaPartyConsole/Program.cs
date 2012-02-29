using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeaParty.Model;

namespace TeaPartyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var guests = new List<Guest>
                             {
                                 new Guest { Name = "Arthur McAdams", Gender = Gender.Male, Status = Status.Noble},
                                 new Guest { Name = "Charlie Sheen", Gender = Gender.Male, Status = Status.Commoner},
                                 new Guest { Name = "Bridgitte Ericsson", Gender = Gender.Female, Status = Status.Commoner},
                                 new Guest { Name = "Jane", Gender = Gender.Female, Status = Status.Noble}
                             };
            var host = new GoodHost();

            Console.WriteLine("The people starts arriving to the party, and the good host attends to his guests: ");
            foreach (var guest in guests)
                Console.WriteLine(host.Welcome(guest));
            Console.WriteLine("Now that everybody here, let the party begin!");
        }
    }
}
