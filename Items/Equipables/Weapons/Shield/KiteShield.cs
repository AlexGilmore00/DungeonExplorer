using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class KiteShield : ParentWeapon
    {
        public KiteShield()
        {
            Name = "Kite Shield";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Lhand";
            Attack = 0;
            Defence = 30;
            IsTwoHanded = false;
            IsShield = true;
            _flavourText = "a sturdy and well made shield the " +
                "covers a good potion of your body. its a wonder " +
                "how something like this got down here";
        }
    }
}
