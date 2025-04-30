using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class FusedSkeletonPhalanx : ParentEnemy
    {
        public FusedSkeletonPhalanx()
        {
            Name = "Fused Skeleton Phalanx";
            MaxHealth = 25;
            Health = MaxHealth;
            CurrentAtkDmg = 12;
            CurrentDefence = 100;
            Attacks = new List<Attack>()
            {
                AttackTypes.BowShot,
                AttackTypes.PoisonShot,
                AttackTypes.Slash,
                AttackTypes.ShieldBash,
                AttackTypes.HeavySlash,
                AttackTypes.HeavyThrust
            };
            ChooseNextAttack();
        }
    }
}
