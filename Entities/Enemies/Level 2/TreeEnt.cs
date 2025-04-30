using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TreeEnt : ParentEnemy
    {
        public TreeEnt()
        {
            Name = "Tree Ent";
            MaxHealth = 40;
            Health = MaxHealth;
            CurrentAtkDmg = 15;
            CurrentDefence = 30;
            Attacks = new List<Attack>()
            {
                AttackTypes.Root,
                AttackTypes.BranchSlap,
                AttackTypes.BrambleWhip
            };
            ChooseNextAttack();
        }
    }
}
