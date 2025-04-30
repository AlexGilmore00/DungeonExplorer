using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Mace : ParentWeapon
    {
        public Mace()
        {
            Name = "Mace";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 8;
            Defence = 0;
            IsTwoHanded = false;
            IsShield = false;
            _flavourText = "a sturdy mace. the head of the mace " +
                "shaped with flanges and looks like it could do " +
                "some serious damage";
        }
    }
}
