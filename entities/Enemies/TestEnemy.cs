using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TestEnemy : ParentEnemy
    {
        public TestEnemy()
        {
            Name = "TestEnemy";
            MaxHealth = 25;
            Health = MaxHealth;
            CurrentAtkDmg = 15;
            CurrentDefence = 5;
        }
    }
}
