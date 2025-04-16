using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentWeapon : ParentEquipable
    {
        public int Attack { get; protected set; }
        // NOTE: two handed weapons should always be given the slot "Rhand"
        public bool IsTwoHanded;
        public bool IsShield;
        public ParentWeapon() { }
    }
}
