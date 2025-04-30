using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class LargeWoodenLid : ParentWeapon
    {
        public LargeWoodenLid()
        {
            Name = "TesterShield";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Lhand";
            Attack = 0;
            Defence = 4;
            IsTwoHanded = false;
            IsShield = true;
            _flavourText = "a shield used for testing";
        }
    }
}
