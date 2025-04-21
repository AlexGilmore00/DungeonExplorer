using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class LivingEntity
    // the master class for both the player and all enemies to inherit from
    {
        public string Name { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health
        {
            get { return _health; }
            protected set
            {
                if (value < 0) { _health = 0; }
                else if (value > MaxHealth) { _health = MaxHealth; }
                else { _health = value; }
            }
        }
        private int _health;
        public int CurrentAtkDmg { get; protected set; }
        public int CurrentDefence { get; protected set; }

        public bool IsDead {  get; protected set; }

        public LivingEntity()
        {
            IsDead = false;
        }
    }
}
