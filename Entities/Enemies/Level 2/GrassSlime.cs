using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GrassSlime : ParentEnemy
    {
        public GrassSlime()
        {
            Name = "Grass Slime";
            MaxHealth = 20;
            Health = MaxHealth;
            CurrentAtkDmg = 15;
            CurrentDefence = 50;
            Attacks = new List<Attack>()
            {
                AttackTypes.Engulf,
                AttackTypes.SlimeShot,
                AttackTypes.SlimeWhip
            };
            ChooseNextAttack();
        }
    }
}
