using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TheHeart : ParentEnemy
    {
        public TheHeart()
        {
            Name = "The Heart";
            MaxHealth = 150;
            Health = MaxHealth;
            CurrentAtkDmg = 30;
            CurrentDefence = 20;
            Attacks = new List<Attack>()
            {
                AttackTypes.ThunderousBeat,
                AttackTypes.BloodShower,
                AttackTypes.ArteryWhip,
                AttackTypes.VeinEntangle
            };
            ChooseNextAttack();
        }
    }
}
