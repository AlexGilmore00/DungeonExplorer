using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class PlateCuirass : ParentArmour
    {
        public PlateCuirass()
        {
            Name = "Plate Cuirass";
            _baseName = Name;
            Category = "Armour";
            Slot = "Chest";
            Defence = 50;
            _flavourText = "strong plate cuirass kept in great condition. " +
                "it must be something with the air here";
        }
    }
}
