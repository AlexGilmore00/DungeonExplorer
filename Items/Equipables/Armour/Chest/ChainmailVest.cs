using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class ChainmailVest : ParentArmour
    {
        public ChainmailVest()
        {
            Name = "Chainmail Vest";
            _baseName = Name;
            Category = "Armour";
            Slot = "Chest";
            Defence = 20;
            _flavourText = "A strong chainmail garment to cover " +
                "your torso. maybe a tad worn but will still " +
                "offer good protection";
        }
    }
}
