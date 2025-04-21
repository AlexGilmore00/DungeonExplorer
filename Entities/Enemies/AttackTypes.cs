using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class AttackTypes
    {
        //
        // using melee weapon
        //

        public static readonly Attack Slash = new Attack("Slash", 1);

        //
        // using shield
        //

        public static readonly Attack ShieldBash = new Attack("Shield Bash", 0.7);

        public AttackTypes()
        {
        }
    }
}
