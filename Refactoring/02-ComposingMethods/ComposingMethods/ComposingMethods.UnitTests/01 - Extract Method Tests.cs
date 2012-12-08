using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ComposingMethods.UnitTests
{
    [TestFixture]
    public class BarbarianTests
    {
        [Test]
        public void Attack_WhenGivenAMonster_ShouldHitMonsterWithHisWeapon()
        {
            // Arrange
            var barbarian = new Barbarian {Name = "Conan", Weapon = new Weapon {Name = "Sword", Damage = 1.5}, HitPoints = 150};
            var monster = new Monster {Name = "Goblin", HitPoints = 10};
            // Act
            string description = barbarian.Attack(monster);
            // Assert
            Assert.That(description, Is.StringContaining("Conan swings his Sword and hits the Goblin in strength. Goblin loses"));
        }

    }
}
