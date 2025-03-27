using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TesterSword : ParentWeapon
    {
        public TesterSword()
        {
            Name = "TesterSword";
            Category = "Weapon";
            Slot = "Rhand";
            Attack = 10;
        }
    }
}
