using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class BowSkeleton : ParentEnemy
    {
        public BowSkeleton()
        {
            Name = "Sword Skeleton";
            MaxHealth = 15;
            Health = MaxHealth;
            CurrentAtkDmg = 9;
            CurrentDefence = 6;
            Attacks = new List<Attack>()
            {
                AttackTypes.BowShot,
                AttackTypes.PoisonShot
            };
            ChooseNextAttack();
        }
    }
}
