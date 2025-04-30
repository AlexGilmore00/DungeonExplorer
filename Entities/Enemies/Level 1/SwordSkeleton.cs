using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonExplorer
{
    public class SwordSkeleton : ParentEnemy
    {
        public SwordSkeleton()
        {
            Name = "Sword Skeleton";
            MaxHealth = 20;
            Health = MaxHealth;
            CurrentAtkDmg = 12;
            CurrentDefence = 6;
            Attacks = new List<Attack>()
            {
                AttackTypes.Slash,
                AttackTypes.HeavyThrust,
            };
            ChooseNextAttack();
        }
    }
}
