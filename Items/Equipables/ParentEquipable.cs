using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentEquipable : ParentItem
    {
        public int Defence { get; protected set; }

        // base name of the weapon to reset to if the name is changed after equipping
        protected string _baseName;

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

        public ParentEquipable() { }

        public void ChangeNameEquip()
        // changes the name of the weapon when equipped
        {
            Name = _baseName + " (equipped)";
        }

        public void ChangeNameUnequip()
        {
            Name = _baseName;
        }
    }
}
