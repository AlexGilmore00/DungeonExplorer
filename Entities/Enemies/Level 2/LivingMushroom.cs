using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LivingMushroom : ParentEnemy
    {
        public LivingMushroom()
        {
            Name = "Living Mushroom";
            MaxHealth = 35;
            Health = MaxHealth;
            CurrentAtkDmg = 20;
            CurrentDefence = 25;
            Attacks = new List<Attack>()
            {
                AttackTypes.CapSmack,
                AttackTypes.Spores
            };
            ChooseNextAttack();
        }
    }
}
