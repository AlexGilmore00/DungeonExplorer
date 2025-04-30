using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TesterSword : ParentWeapon
    {
        public TesterSword()
        {
            Name = "TesterSword";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 6;
            Defence = 0;
            IsTwoHanded = false;
            IsShield = false;
            _flavourText = "a sword used for testing";
        }
    }
}
