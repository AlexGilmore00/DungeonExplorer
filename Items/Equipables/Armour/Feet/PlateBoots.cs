using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class PlateBoots : ParentArmour
    {
        public PlateBoots()
        {
            Name = "Plate Boots";
            _baseName = Name;
            Category = "Armour";
            Slot = "Feet";
            Defence = 20;
            _flavourText = "strong plate boots kept in great condition. " +
                "it must be something with the air here";
        }
    }
}
