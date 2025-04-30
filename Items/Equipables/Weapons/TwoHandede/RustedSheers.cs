using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class RustedSheers : ParentWeapon
    {
        public RustedSheers()
        {
            Name = "Rusted Sheers";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 4;
            Defence = 0;
            IsTwoHanded = true;
            IsShield = false;
            _flavourText = "an old pair of rusted shield, likely not " +
                "used for some years";
        }
    }
}
