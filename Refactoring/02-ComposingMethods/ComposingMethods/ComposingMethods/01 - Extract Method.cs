using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposingMethods
{

    /* Extract Method
     * 
     * Problem: You have a code fragment that can be grouped together.
     * 
     * Refactoring: Turn the fragment into a method whose name explains the purpose of the method
     * 
     * 
     * 
     */

    public class Character
    {
        public double HitPoints { get; set; }
    }

    public class Die10
    {
        private Random random = new Random();

        public int Roll()
        {
            return random.Next(1, 10);
        }
    }

    public class Barbarian : Character
    {
        public string Name { get; set; }
        public Weapon Weapon { get; set; }

        public string Describe()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Hither came Conan, the Cimmerian, black-haired, sullen-eyed, sword in hand, a thief, a reaver, a slayer, with gigantic melancholies and gigantic mirth, to tread the jeweled thrones of the Earth under his sandalled feet.");
            return sb.ToString();
        }

        public string Attack(Monster monster)
        {
            Die10 die = new Die10();
            double damageInflicted = die.Roll() + die.Roll() + Weapon.Damage;
            monster.HitPoints -= damageInflicted;
            return string.Format("{0} swings his {1} and hits the {2} in strength. Goblin loses {3} HP.", Name, Weapon, monster, damageInflicted);
        }
    }

    public class Weapon
    {
        public string Name { get; set; }
        public double Damage { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Monster : Character
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    // Refactored Version
    // public class Barbarian : Character
    // {
    //     public string Name { get; set; }
    //     public Weapon Weapon { get; set; }

    //     public string Describe()
    //     {
    //         StringBuilder sb = new StringBuilder();
    //         sb.Append("Hither came Conan, the Cimmerian, black-haired, sullen-eyed, sword in hand, a thief, a reaver, a slayer, with gigantic melancholies and gigantic mirth, to tread the jeweled thrones of the Earth under his sandalled feet.");
    //         return sb.ToString();
    //     }

    //     public string Attack(Monster monster)
    //     {
    //         double damageInflicted = CalculateDamageInflicted();
    //         monster.HitPoints -= damageInflicted;
    //         return string.Format("{0} swings his {1} and hits the {2} in strength. Goblin loses {3} HP.", Name, Weapon, monster, damageInflicted);
    //     }

    //     /// Extracted method for calculating inflicted damage
    //     private double CalculateDamageInflicted()
    //     {
    //         Die10 die = new Die10();
    //         double damageInflicted = die.Roll() + die.Roll() + Weapon.Damage;
    //         return damageInflicted;
    //     }
    //}
    


}
