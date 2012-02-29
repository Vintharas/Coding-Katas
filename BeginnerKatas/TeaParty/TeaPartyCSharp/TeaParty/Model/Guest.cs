namespace TeaParty.Model
{
    public class Guest
    {
        private const int LASTNAME_INDEX = 1;

        public string Name { get; set; }
        public string LastName
        {
            get 
            { 
                string[] nameParticles = Name.Split();
                return nameParticles.Length > 1 ? nameParticles[LASTNAME_INDEX] : "X";
            }
        }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}