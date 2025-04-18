using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentItem
    {
        public string Name;

        protected string _flavourText;

        // stores what category the item is a part of...
        // "Weapon", "Armour", "Consumable"
        public string Category
        {
            get { return _category; }
            protected set
            {
                // make sure the item has a valid category
                if (!_validCategories.Contains(value))
                {
                    Console.WriteLine($"WARNING! {this.Name} has an invalid category type and must be fixed immediately");
                    _category = "Weapon";
                }
                else { _category = value; }
            }
        }
        private string _category;
        private HashSet<string> _validCategories = new HashSet<string> { "Weapon", "Armour", "Consumable" };

        public ParentItem() { }

        public abstract void DisplayDescription();
    }
}
