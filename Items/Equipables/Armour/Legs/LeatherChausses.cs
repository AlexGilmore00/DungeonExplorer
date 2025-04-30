using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LeatherChausses : ParentArmour
    {
        public LeatherChausses()
        {
            Name = "Leather Chausses";
            _baseName = Name;
            Category = "Armour";
            Slot = "Legs";
            Defence = 13;
            _flavourText = "decently kept leather trousers. slightly " +
                "restrictive but offer good defence against cuts";
        }
    }
}
