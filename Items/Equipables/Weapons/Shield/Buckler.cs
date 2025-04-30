using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Buckler : ParentWeapon
    {
        public Buckler()
        {
            Name = "Buckler";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Lhand";
            Attack = 0;
            Defence = 10;
            IsTwoHanded = false;
            IsShield = true;
            _flavourText = "a light buckler shield. Small " +
                "and handy to carry but still very effective";
        }
    }
}
