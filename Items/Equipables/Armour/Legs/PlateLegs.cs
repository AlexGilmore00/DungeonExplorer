using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class PlateLegs : ParentArmour
    {
        public PlateLegs()
        {
            Name = "Plate Legs";
            _baseName = Name;
            Category = "Armour";
            Slot = "Legs";
            Defence = 20;
            _flavourText = "strong plate Leggings kept in great condition. " +
                "it must be something with the air here";
        }
    }
}
