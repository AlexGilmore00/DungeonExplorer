using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TesterPotion : ParentConsumable
    {
        public TesterPotion()
        {
            Name = "TesterPotion";
            Category = "Consumable";
            _effectDescription = "heals 15 hp";
            _flavourText = "a potion used for testing";
        }
    }
}
