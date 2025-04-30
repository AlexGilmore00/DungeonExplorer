using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TestBoss : ParentEnemy
    {
        public TestBoss()
        {
            Name = "TestBoss";
            MaxHealth = 100;
            Health = MaxHealth;
            CurrentAtkDmg = 20;
            CurrentDefence = 8;
            Attacks = new List<Attack>()
            {
                AttackTypes.Slash,
                AttackTypes.ShieldBash,
                AttackTypes.HeavyThrust,
                AttackTypes.HeavySlash
            };
            ChooseNextAttack();
        }
    }
}
