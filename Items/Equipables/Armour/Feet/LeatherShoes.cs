using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LeatherShoes : ParentArmour
    {
        public LeatherShoes()
        {
            Name = "Leather Shoes";
            _baseName = Name;
            Category = "Armour";
            Slot = "Feet";
            Defence = 2;
            _flavourText = "simple leather shoes";
        }
    }
}
