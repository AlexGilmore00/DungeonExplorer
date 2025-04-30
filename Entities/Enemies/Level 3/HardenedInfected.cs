using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class HardenedInfected : ParentEnemy
    {
        public HardenedInfected()
        {
            Name = "Hardened Infected";
            MaxHealth = 30;
            Health = MaxHealth;
            CurrentAtkDmg = 25;
            CurrentDefence = 120;
            Attacks = new List<Attack>()
            {
                AttackTypes.Root,
                AttackTypes.LabouredPunch,
                AttackTypes.InfectedScratch
            };
            ChooseNextAttack();
        }
    }
}
