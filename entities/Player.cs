using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : LivingEntity
    {
        private Dictionary<string, List<string>> inventory = new Dictionary<string, List<string>>
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
        {
            foreach (var category in inventory)
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