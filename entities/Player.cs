using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : LivingEntity
    {
        private Dictionary<string, List<string>> _inventory = new Dictionary<string, List<string>>
        {
            {"weapons", new List<string> { "sword", "shield" } },
            {"armour", new List<string> { "cloth trousers", "cloth shirt" } },
            {"consumables", new List<string> { "health potion" } },
            {"misc", new List<string> { "rusted key" } }
        };

        public Player(string name, int maxHealth) 
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }
        public void PickUpItem(string item)
        {
            
        }
        public void DisplayInventoryContents()
        // prints the whole contents of the players inventoy to the console
        // items are displayed in chunks with headers depenting opn what sort of item they are
        // weapon, armour, etc...
        // the _inventory variable is a dictionary where the keys are the item categories and
        // the values are a list of all the items in that category that the player currently has
        {
            foreach (var category in _inventory)
            {
                Console.WriteLine(category.Key.ToUpper() + ":");
                int counter = 1;
                foreach (var item in category.Value)
                {
                    Console.WriteLine($"{counter}. {item}");
                    counter++;
                }
            }
        }
    }
}