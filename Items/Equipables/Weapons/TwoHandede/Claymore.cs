using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Claymore : ParentWeapon
    {
        public Claymore()
        {
            Name = "Claymore";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 30;
            Defence = 15;
            IsTwoHanded = true;
            IsShield = false;
            _flavourText = "a greatly crafted longsword. the " +
                "balance of the sword is great and so can easily " +
                "be used to dish out damage as well as defend " +
                "yourself";
        }
    }
}
