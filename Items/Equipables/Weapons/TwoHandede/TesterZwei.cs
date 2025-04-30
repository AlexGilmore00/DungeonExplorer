using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DungeonExplorer
{
    internal class TesterZwei : ParentWeapon
    {
        public TesterZwei()
        {
            Name = "TesterZwei";
            _baseName = Name;
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 10;
            Defence = 0;
            IsTwoHanded = true;
            IsShield = false;
            _flavourText = "a zweihander used for testing";
        }
    }
}
