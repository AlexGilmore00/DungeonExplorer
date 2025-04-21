using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public readonly struct Attack
    {
        public readonly string Name;
        public readonly double DmgMod;
        public readonly bool IsCharge;
        public readonly int ChargeTurns;
        public readonly string ChargeMessage;
        public readonly bool AppliesStatus;
        public readonly string Status;

        public Attack(string name, double dmgMod, 
            bool isCharge = false, int chargeTurns = 0, string chargeMessage = "", 
            bool appliesStatus = false, string status = "")
        // name is the name of the attack
        // dmgMod is a decimal value that is multiplies with the Enemy's damage to get
        // the finale damage of the attack
        // isCharge denotes if the attack should be charged before coming out
        // appliesStatus says whether the attack should apply a status effect if it hits
        // NOTE: if isCharge is true, values should also be given for chargeTurns
        // and chargeMessage
        // NOTE: if appliesStatus is true, values should also be given for status
        {
            Name = name;
            DmgMod = dmgMod;
            IsCharge = isCharge;
            ChargeTurns = chargeTurns;
            ChargeMessage = chargeMessage;
            AppliesStatus = appliesStatus;
            Status = status;

            // clean bools with default values
            if (IsCharge && ChargeTurns == 0)
            {
                ChargeTurns = 1;
            }
            if (IsCharge && string.IsNullOrEmpty(ChargeMessage))
            {
                ChargeMessage = $"Charging {Name}";
            }
            if (AppliesStatus && string.IsNullOrEmpty(Status))
            {
                Status = "Bleed";
            }
        }
    }
}
