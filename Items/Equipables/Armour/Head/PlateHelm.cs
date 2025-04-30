using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class PlateHelm : ParentArmour
    {
        public PlateHelm()
        {
            Name = "Plate Helm";
            _baseName = Name;
            Category = "Armour";
            Slot = "Head";
            Defence = 20;
            _flavourText = "strong plate Helm kept in great condition. " +
                "it must be something with the air here";
        }
    }
}
