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
        public static readonly Attack HeavyThrust = new Attack("Heavy Thrust", 1.8,
            isCharge: true, chargeTurns: 1, chargeMessage: "winds back their sword arm");
        public static readonly Attack HeavySlash = new Attack("Heavy Slash", 1.2,
            isCharge: true, chargeTurns: 1, chargeMessage: "raises their sword arm",
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 1, statusDuration: 2);

        //
        // using shield
        //

        public static readonly Attack ShieldBash = new Attack("Shield Bash", 0.7,
            appliesStatus: true, status: StatusIds.Stun, statusChance: 0.3, statusDuration: 1);

        public AttackTypes()
        {
        }
    }
}
