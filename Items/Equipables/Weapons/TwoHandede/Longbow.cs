using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Longbow : ParentWeapon
    {
        public Longbow()
        {
            Name = "Longbow";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 16;
            Defence = 0;
            IsTwoHanded = true;
            IsShield = false;
            _flavourText = "a european style longbow. it " +
                "comes with a stash of arrows, seemingly " +
                "more than you'll ever need";
        }
    }
}
