using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Abomination : ParentEnemy
    {
        public Abomination()
        {
            Name = "Abomination";
            MaxHealth = 70;
            Health = MaxHealth;
            CurrentAtkDmg = 30;
            CurrentDefence = 13;
            Attacks = new List<Attack>()
            {
                AttackTypes.WailingCry,
                AttackTypes.GroundSlam,
                AttackTypes.LabouredPunch,
            };
            ChooseNextAttack();
        }
    }
}
