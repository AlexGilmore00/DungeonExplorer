using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class ClothTrousers : ParentArmour
    {
        public ClothTrousers()
        {
            Name = "Cloth Trousers";
            _baseName = Name;
            Category = "Armour";
            Slot = "Legs";
            Defence = 5;
            _flavourText = "light cloth trousers. feel nice " +
                "to move around in";
        }
    }
}
