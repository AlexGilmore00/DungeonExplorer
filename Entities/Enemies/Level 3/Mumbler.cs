using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Mumbler : ParentEnemy
    {
        public Mumbler()
        {
            Name = "Mumbler";
            MaxHealth = 40;
            Health = MaxHealth;
            CurrentAtkDmg = 20;
            CurrentDefence = 25;
            Attacks = new List<Attack>()
            {
                AttackTypes.Spores,
                AttackTypes.WailingCry,
                AttackTypes.DesperateScratch,
            };
            ChooseNextAttack();
        }
    }
}
