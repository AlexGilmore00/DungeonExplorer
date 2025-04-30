using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GiantGiardDog : ParentEnemy
    {
        public GiantGiardDog()
        {
            Name = "Giant Guard Dog";
            MaxHealth = 80;
            Health = MaxHealth;
            CurrentAtkDmg = 20;
            CurrentDefence = 20;
            Attacks = new List<Attack>()
            {
                AttackTypes.TailWhip,
                AttackTypes.Scratch,
                AttackTypes.Bite,
            };
            ChooseNextAttack();
        }
    }
}
