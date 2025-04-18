using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TesterHelm : ParentArmour
    {
        public TesterHelm()
        {
            Name = "TesterHelm";
            _baseName = Name;
            Category = "Armour";
            Slot = "Head";
            Defence = 5;
            _flavourText = "a helmet used for testing";
        }
    }
}
