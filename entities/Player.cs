using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DungeonExplorer
{
    public class Player : LivingEntity
    {
        private List<ParentWeapon> _invWeapons = new List<ParentWeapon>();
        private List<ParentArmour> _invArmour = new List<ParentArmour>();
        private List<ParentConsumable> _invComsumables = new List<ParentConsumable>();
        // dicts to keep track of what items the player has equipped
        //keys correspond exactly to an items slot member
        private Dictionary<string, ParentArmour> _eqArmour = new Dictionary<string, ParentArmour>
        {
            { "Head", null },
            { "Chest", null },
            { "Legs", null },
            { "Feet", null }
        };
        private Dictionary<string, ParentWeapon> _eqWeapon = new Dictionary<string, ParentWeapon>
        {
            { "Rhand", null },
            { "Lhand", null }
        };

        public Player(string name, int maxHealth, int baseDmg, int BaseDef) 
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;

            // base values for attack and defence
            CurrentAtkDmg = baseDmg;
            CurrentDefence = BaseDef;
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
                        _invWeapons.Add(wepItem);
                        break;
                    case "Armour":
                        ParentArmour armItem = (ParentArmour)item;
                        _invArmour.Add(armItem);
                        break;
                    case "Consumable":
                        ParentConsumable conItem = (ParentConsumable)item;
                        _invComsumables.Add(conItem);
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
            foreach (var weapon in _invWeapons)
            {
                Console.WriteLine($"{counter}. {weapon.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("ARMOUR");
            foreach (var armour in _invArmour)
            {
                Console.WriteLine($"{counter}. {armour.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("CONSUMABLES");
            foreach (var consumable in _invComsumables)
            {
                Console.WriteLine($"{counter}. {consumable.Name}");
                counter++;
            }
        }

        public void TryEquipItem(ParentItem item)
        // try cast a given item to its relevant child type and proceed to
        // equipping it if the valid slot is in a state to have something equipped to it
        {
            try
            {
                switch (item.Category)
                {
                    case "Weapon":
                        ParentWeapon wepItem = (ParentWeapon)item;
                        // check if the weapon is two handed and will override the right hand weapon
                        if (wepItem.IsTwoHanded && _eqWeapon["Lhand"] != null)
                        {
                            Console.WriteLine($"this weapon requires two hands to wield and so equipping it will " +
                                $"unequip the {_eqWeapon["Lhand"].Name} in your left hand");
                            // !!ADD FUNCTIONALITY FOR A CONFIRM FOR THIS OPTION!!
                            UnequipItem("Lhand");
                        }
                        // check if equipping a left hand weapon will need to override a two handed
                        // weapon in the right hand
                        if (_eqWeapon["Lhand"] != null)
                        {
                            if (wepItem.Slot == "Lhand" && _eqWeapon["Lhand"].IsTwoHanded)
                            {
                                Console.WriteLine($"equipping this weapon in your left hand will uneqiup your " +
                                    $"{_eqWeapon["Lhand"].Name} as this weapon required two hands to wield");
                                // !!ADD FUNCTIONALITY FOR A CONFIRM FOR THIS OPTION!!
                                UnequipItem("Lhand");
                            }
                        }
                        // check if there is still already an item in the desired slot that needs to be removed
                        if (_eqWeapon[wepItem.Slot] != null)
                        {
                            UnequipItem(wepItem.Slot);
                        }
                        // equip the weapon
                        EquipWeapon(wepItem);
                        break;
                    case "Armour":
                        ParentArmour armItem = (ParentArmour)item;
                        // check if there is already an item in that slot that needs to be unequipped
                        if (_eqArmour[armItem.Slot] != null)
                        {
                            UnequipItem(armItem.Slot);
                        }
                        // equip the armour
                        EquipArmour(armItem);
                        break;
                    default:
                        Console.WriteLine($"WARNING! unexpected error occured when trying to equip " +
                            $"{item.Name}. Its category field had an invalid value.");
                        break;
                }
            }
            catch (InvalidCastException)
            {
                Console.WriteLine($"WARNING! unexpected error occured when equipping {item.Name}" +
                    $". its category field does not match its type");
            }
            catch (Exception e)
            {
                Console.WriteLine($"WARNING! unexpected error when equipping {item.Name}" +
                    $". exception: {e}\n{e.Message}");
            }
        }

        private void EquipWeapon(ParentWeapon weapon)
        // equips a weapon to to the player
        {
            // add this weapons stats to the player
            CurrentAtkDmg += weapon.Attack;
            CurrentDefence += weapon.Defence;

            // equip the weapon to the right slot
            _eqWeapon[weapon.Slot] = weapon;
            // also equip it in the left hand if the weapon is two handed
            if (weapon.IsTwoHanded)
            {
                _eqWeapon["Lhand"] = weapon;
            }
        }

        private void EquipArmour(ParentArmour armour)
        // equips armour to the player
        {
            // add the armours stats to the player
            CurrentDefence += armour.Defence;

            // equip the armour
            _eqArmour[armour.Slot] = armour;
        }

        public void UnequipItem(string slot)
        // unequips the item in the given slot
        {
            // weapons
            if (slot == "Rhand" | slot == "Lhand")
            {
                // remove the stats from the player
                CurrentAtkDmg -= _eqWeapon[slot].Attack;
                CurrentDefence -= _eqWeapon[slot].Defence;

                // check if the weapon is two handed
                if (_eqWeapon[slot].IsTwoHanded)
                {
                    // if so, unequip it from both hands
                    _eqWeapon["Rhand"] = null;
                    _eqWeapon["Lhand"] = null;
                }
                else
                {
                    // otherwise just unequip it from the equipped hand
                    _eqWeapon[slot] = null;
                }
            }
            // armour
            else if (slot == "Head" | slot == "Chest" | slot == "Legs" | slot == "Feet")
            {
                // remove the stats from the player
                CurrentDefence -= _eqArmour[slot].Defence;

                // unequip the armour
                _eqArmour[slot] = null;
            }
            else
            {
                Console.WriteLine("WARNING!! when trying to unequip an item, an invalid slot was passed." +
                    "this means the stats given by the item have not been removed from the player despite" +
                    "the item likely no longer being in the players equipped dict.");
            }
        }

        public Dictionary<string, ParentWeapon> GetEquippedWeapons()
        {
            return _eqWeapon;
        }
        
        public Dictionary<string, ParentArmour> GetEquippedArmour()
        {
            return _eqArmour;
        }
    }
}