using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class StrawHat : ParentArmour
    {
        public StrawHat()
        {
            Name = "Straw Hat";
            _baseName = Name;
            Category = "Armour";
            Slot = "Head";
            Defence = 2;
            _flavourText = "a helmet used for testing";
        }
    }
}
