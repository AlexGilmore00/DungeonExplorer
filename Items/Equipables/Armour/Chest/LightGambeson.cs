using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LightGambeson : ParentArmour
    {
        public LightGambeson()
        {
            Name = "Light Gambeson";
            _baseName = Name;
            Category = "Armour";
            Slot = "Chest";
            Defence = 5;
            _flavourText = "a light gambeson that isnt too cumbersome to " +
                "wear. still feel like it offer some good protection though";
        }
    }
}
