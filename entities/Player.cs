using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DungeonExplorer
{
    public class Player : LivingEntity
    {
        private List<ParentWeapon> _InvWeapons = new List<ParentWeapon>();
        private List<ParentArmour> _InvArmour = new List<ParentArmour>();
        private List<ParentConsumable> _InvComsumables = new List<ParentConsumable>(); 

        public Player(string name, int maxHealth) 
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;

            // TEMPORARY VALUES
            CurrentAtkDmg = 11;
            CurrentDefence = 5;
        }
        public void PickUpItem(ParentItem item)
        // takes an item as input, casts it to the relevant child class, and adds
        // it to the relevant inventory list
        {
            try
            {
                switch (item.Category)
                {
                    case "Weapon":
                        ParentWeapon wepItem = (ParentWeapon)item;
                        _InvWeapons.Add(wepItem);
                        break;
                    case "Armour":
                        ParentArmour armItem = (ParentArmour)item;
                        _InvArmour.Add(armItem);
                        break;
                    case "Consumable":
                        ParentConsumable conItem = (ParentConsumable)item;
                        _InvComsumables.Add(conItem);
                        break;
                    default:
                        Console.WriteLine($"WARNING! unexpected error occured when trying to add " +
                            $"{item.Name} to inventory. Its category field had an invalid value.");
                        break;
                }
            }
            catch (InvalidCastException)
            {
                Console.WriteLine($"WARNING! unexpected error occured when adding {item.Name} " +
                    $"to inventory. its category field does not match its type");
            }
            catch(Exception e)
            {
                Console.WriteLine($"WARNING! unexpected error when adding {item.Name} " +
                    $"to inventory. exception: {e}\n{e.Message}");
            }
        }
        public void DisplayInventoryContents()
        // prints the whole contents of the players inventoy to the console
        // items are displayed in chunks with headers depenting opn what sort of item they are
        // weapon, armour, etc...
        {
            int counter = 1;
            Console.WriteLine("WEAPONS");
            foreach (var weapon in _InvWeapons)
            {
                Console.WriteLine($"{counter}. {weapon.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("ARMOUR");
            foreach (var armour in _InvArmour)
            {
                Console.WriteLine($"{counter}. {armour.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("CONSUMABLES");
            foreach (var consumable in _InvComsumables)
            {
                Console.WriteLine($"{counter}. {consumable.Name}");
                counter++;
            }
        }
    }
}