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
        public readonly StatusIds Status;
        public readonly double StatusChance;
        public readonly int StatusDuration;

        public Attack(string name, double dmgMod, 
            bool isCharge = false, int chargeTurns = 0, string chargeMessage = "", 
            bool appliesStatus = false, StatusIds status = 0, double statusChance = 0, int statusDuration = 0)
        // name is the name of the attack
        // dmgMod is a decimal value that is multiplies with the Enemy's damage to get
        // the finale damage of the attack
        // isCharge denotes if the attack should be charged before coming out
        // appliesStatus says whether the attack should apply a status effect if it hits
        // statusChance is a number between 0 and 1 and is the chance a ststus is applied on a hit
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
            StatusChance = statusChance;
            StatusDuration = statusDuration;

            // clean bools with default values
            if (IsCharge && ChargeTurns == 0)
            {
                ChargeTurns = 1;
            }
            if (IsCharge && string.IsNullOrEmpty(ChargeMessage))
            {
                ChargeMessage = $"Charging {Name}";
            }
            if (StatusChance < 0)
            {
                StatusChance = 0;
            }
            if (StatusChance > 1)
            {
                StatusChance = 1;
            }
            if (AppliesStatus && StatusDuration == 0)
            {
                StatusDuration = 1;
            }
        }
    }
}
