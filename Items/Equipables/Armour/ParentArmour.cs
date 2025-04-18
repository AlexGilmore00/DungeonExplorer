using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentArmour : ParentEquipable
    {
        public ParentArmour() { }

        public override void DisplayDescription()
        {
            Console.WriteLine($"{Name}:\n" +
                $"Defence: {Defence}\n" +
                $"Slot: {Slot}\n" +
                $"{_flavourText}\n");
        }
    }
}
