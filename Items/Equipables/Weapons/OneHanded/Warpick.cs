using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Warpick : ParentWeapon
    {
        public Warpick()
        {
            Name = "Warpick";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 19;
            Defence = 0;
            IsTwoHanded = false;
            IsShield = false;
            _flavourText = "a warpick with a very menacing spike " +
                "on the end. would have been great for piercing " +
                "opponents armour... not that youll be seeing many " +
                "humans down here";
        }
    }
}
