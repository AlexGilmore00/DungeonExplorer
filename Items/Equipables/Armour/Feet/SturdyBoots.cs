using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class SturdyBoots : ParentArmour
    {
        public SturdyBoots()
        {
            Name = "Sturdy Boots";
            _baseName = Name;
            Category = "Armour";
            Slot = "Feet";
            Defence = 7;
            _flavourText = "some well made boots to help your " +
                "feet on this treck";
        }
    }
}
