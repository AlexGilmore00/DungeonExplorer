using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentWeapon : ParentEquipable, INeedsDescription
    {
        public int Attack { get; protected set; }
        // NOTE: two handed weapons should always be given the slot "Rhand"
        public bool IsTwoHanded;
        public bool IsShield;
        public ParentWeapon() { }

        public void DisplayDescription()
        {
            Console.WriteLine($"{Name}:\n" +
                $"Attack: {Attack}\n" +
                $"Defence: {Defence}\n" +
                $"Two handed: {IsTwoHanded}\n" +
                $"Shield: {IsShield}\n" +
                $"Slot: {Slot}\n" +
                $"{_flavourText}\n");
        }
    }
}
