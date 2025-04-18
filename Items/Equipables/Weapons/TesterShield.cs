using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class TesterShield : ParentWeapon
    {
        public TesterShield()
        {
            Name = "TesterShield";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Lhand";
            Attack = 0;
            Defence = 2;
            IsTwoHanded = false;
            IsShield = false;
            _flavourText = "a shield used for testing";
        }
    }
}
