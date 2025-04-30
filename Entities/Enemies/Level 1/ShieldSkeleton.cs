using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonExplorer
{
    public class ShieldSkeleton : ParentEnemy
    {
        public ShieldSkeleton()
        {
            Name = "Shield Skeleton";
            MaxHealth = 20;
            Health = MaxHealth;
            CurrentAtkDmg = 9;
            CurrentDefence = 15;
            Attacks = new List<Attack>()
            {
                AttackTypes.Slash,
                AttackTypes.ShieldBash,
                AttackTypes.HeavySlash
            };
            ChooseNextAttack();
        }
    }
}
