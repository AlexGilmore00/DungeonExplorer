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
            Category = "Armour";
            Slot = "Head";
            Defence = 10;
        }
    }
}
