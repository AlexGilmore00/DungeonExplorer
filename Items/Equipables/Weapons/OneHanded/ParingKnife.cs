using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class ParingKnife : ParentWeapon
    {
        public ParingKnife()
        {
            Name = "Paring Knife";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 1;
            Defence = 0;
            IsTwoHanded = false;
            IsShield = false;
            _flavourText = "a little Paring knife, good for apples";
        }
    }
}
