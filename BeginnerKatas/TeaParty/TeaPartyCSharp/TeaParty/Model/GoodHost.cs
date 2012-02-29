using System.Text;
using TeaParty.Interfaces;

namespace TeaParty.Model
{
    public class GoodHost : IHost
    {
        /// <summary>
        /// Welcome a guest
        /// </summary>
        /// <param name="guest">Guest</param>
        /// <returns>An appropriate greeting message tailored for each guest</returns>
        public string Welcome(Guest guest)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Hello ");
            if (guest.Gender == Gender.Female)
                sb.Append(guest.Status == Status.Commoner ? "Ms. " : "Lady ");
            else
                sb.Append(guest.Status == Status.Commoner ? "Mr. " : "Sir ");
            sb.Append(guest.LastName);
            return sb.ToString();
        }
    }
}