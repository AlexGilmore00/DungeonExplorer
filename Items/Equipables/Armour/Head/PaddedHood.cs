using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class PaddedHood : ParentArmour
    {
        public PaddedHood()
        {
            Name = "Padded Hood";
            _baseName = Name;
            Category = "Armour";
            Slot = "Head";
            Defence = 10;
            _flavourText = "a padded hood great for protecting your head";
        }
    }
}
