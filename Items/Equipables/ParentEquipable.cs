using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentEquipable : ParentItem
    {
        // which slot the item should be equiped in...
        // "Head", "Chest", "Legs", "Feet", "Rhand", "Lhand"
        public string Slot
        {
            get { return _slot; }
            protected set
            {
                // make sure every has a valid slot type
                if (!_validSlots.Contains(value))
                {
                    Console.WriteLine($"WARNING! {this.Name} has an invalid slot type and must be fixed immediately");
                    _slot = "Head";
                }
                else { _slot = value; }
            }
        }
        private string _slot;
        private HashSet<string> _validSlots = new HashSet<string> {
            "Head", "Chest", "Legs", "Feet", "Rhand", "Lhand" };
        public int Attack {  get; protected set; }
        public int Defence { get; protected set; }

        public ParentEquipable() { }
    }
}
