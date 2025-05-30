﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace DungeonExplorer
{
    public class Player : LivingEntity, IDamageable, IEntityNeedsDescription
    {
        public List<ParentWeapon> InvWeapons { get; private set; }
        public List<ParentArmour> InvArmour { get; private set; }
        public List<ParentConsumable> InvConsumables { get; private set; }
        // dicts to keep track of what items the player has equipped
        // keys correspond exactly to an items slot member
        public Dictionary<string, ParentArmour> EqArmour { get; private set; }
        public Dictionary<string, ParentWeapon> EqWeapon {  get; private set; }

        public Player(string name, int maxHealth, int baseDmg, int BaseDef) 
        {
            // set up inventory
            InvWeapons = new List<ParentWeapon>();
            InvArmour = new List<ParentArmour>();
            InvConsumables = new List<ParentConsumable>();

            // set up equipped items
            EqArmour = new Dictionary<string, ParentArmour>
            {
                { "Head", null },
                { "Chest", null },
                { "Legs", null },
                { "Feet", null }
            };
            EqWeapon = new Dictionary<string, ParentWeapon>
            {
                { "Rhand", null },
                { "Lhand", null }
            };

            Name = name;
            MaxHealth = maxHealth;
            Health = MaxHealth;

            // base values for attack and defence
            CurrentAtkDmg = baseDmg;
            CurrentDefence = BaseDef;
        }

        public void DealDamageTo(Player player, ParentEnemy enemy)
        {
            int attack = player.CurrentAtkDmg;
            int defence = enemy.CurrentDefence;

            double damageReduction = (double)defence / ((double)defence + 50);
            double damageDouble = attack * (1 - damageReduction);
            int damage = (int)damageDouble;

            // deal a minimum of 1 damage
            if (damage <= 0) { damage = 1; }

            enemy.TakeDamage(damage);
            Console.WriteLine($"{enemy.Name} took {damage} damage");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            // kill the entity if their health is 0 or below
            if (Health <= 0)
            {
                IsDead = true;
            }
        }

        public void DisplayEntityDescription()
        // display player stats
        {
            // set up the players equipped items
            string head;
            if (EqArmour["Head"] != null) { head = EqArmour["Head"].Name; } else { head = "nothing"; }
            string chest;
            if (EqArmour["Chest"] != null) { chest = EqArmour["Chest"].Name; } else { chest = "nothing"; }
            string legs;
            if (EqArmour["Legs"] != null) { legs = EqArmour["Legs"].Name; } else { legs = "nothing"; }
            string feet;
            if (EqArmour["Feet"] != null) { feet = EqArmour["Feet"].Name; } else { feet = "nothing"; }
            string rhand;
            if (EqWeapon["Rhand"] != null) { rhand = EqWeapon["Rhand"].Name; } else { rhand = "nothing"; }
            string lhand;
            if (EqWeapon["Lhand"] != null) { lhand = EqWeapon["Lhand"].Name; } else { lhand = "nothing"; }

            // get damage reduction
            double DamageReduction = (double)CurrentDefence / ((double)CurrentDefence + 50);
            DamageReduction *= 100;
            DamageReduction = Math.Round(DamageReduction, 1);

            Console.WriteLine($"name: {Name}\n" +
                $"health: {Health}/{MaxHealth}\n" +
                $"Current Damage: {CurrentAtkDmg}\n" +
                $"Current Defence: {CurrentDefence}\n" +
                $"Current Damage Reduction: {DamageReduction}%" +
                $"\n" +
                $"EQUIPPED ITEMS\n" +
                $"head: {head}\n" +
                $"chest: {chest}\n" +
                $"legs: {legs}\n" +
                $"feel: {feet}\n" +
                $"right hand: {rhand}\n" +
                $"left hand: {lhand}\n" +
                $"\n" +
                $"STATUS EFFECTS\n");
            foreach (var status in StatusEffects)
            {
                Console.WriteLine($"{status.Name}: {status.Duration} turns");
            }
            Console.WriteLine();
        }

        public void PickUpItem(ParentItem item, bool remove = false)
        // takes an item as input, casts it to the relevant child class, and adds
        // it to the relevant inventory list
        // the bool remove can instead be set to true to remove an item from the players inventory
        // !!MAYBE ADD CHECKS FOR IF AN UNPRESENT ITEM IS ATTEMPLTED TO BE REMOVED!!
        {
            try
            {
                switch (item.Category)
                {
                    case "Weapon":
                        ParentWeapon wepItem = (ParentWeapon)item;
                        if (!remove)
                            InvWeapons.Add(wepItem);
                        else
                            InvWeapons.Remove(wepItem);
                        break;
                    case "Armour":
                        ParentArmour armItem = (ParentArmour)item;
                        if (!remove)
                            InvArmour.Add(armItem);
                        else
                            InvArmour.Remove(armItem);
                        break;
                    case "Consumable":
                        ParentConsumable conItem = (ParentConsumable)item;
                        if (!remove)
                            InvConsumables.Add(conItem);
                        else
                            InvConsumables.Remove(conItem);
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

        public void SetInventory(List<ParentItem> itemList, string category)
        // used to set a certain category of a players inventory to a specific state
        // clears that section of the inventory before setting it to the inputted list
        // itemList must be filled with items of the same category
        // category must be either: "Weapon", "Armour", "Consumable"
        // in accordance to the ParentItem Category setter
        {
            // clear the correct inventory
            if (category == "Weapon")
                InvWeapons.Clear();
            else if (category == "Armour")
                InvArmour.Clear();
            else if (category == "Consumable")
                InvConsumables.Clear();
            else
            {
                Console.WriteLine($"WARNING!! an invalid category of {category} " +
                    $"was given when calling Player.SetInventory");
            }

            // check if the item list is empty
            if (itemList.Count <= 0)
            {
                return;
            }

            try
            {
                // convert the list to the correct type and set
                // the correct inventory equal to it
                if (category == "Weapon")
                {
                    InvWeapons = itemList.ConvertAll(x => (ParentWeapon)x);
                }
                else if (category == "Armour")
                {
                    InvArmour = itemList.ConvertAll(x => (ParentArmour)x);
                }
                else if (category == "Consumable")
                {
                    InvConsumables = itemList.ConvertAll(x => (ParentConsumable)x);
                }
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("WARNING!! a list of items that weere not all of the same " +
                    "category was given to Player.SetInventory");
            }
            catch (Exception e)
            {
                Console.WriteLine($"WARNING!! unexpected error occured when calling Player." +
                    $"SetInventory: {e}\n{e.Message}");
            }
        }

        public void DisplayInventoryContents()
        // prints the whole contents of the players inventoy to the console
        // items are displayed in chunks with headers depenting opn what sort of item they are
        // weapon, armour, etc...
        {
            int counter = 1;
            Console.WriteLine("WEAPONS");
            foreach (var weapon in InvWeapons)
            {
                Console.WriteLine($"{counter}. {weapon.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("ARMOUR");
            foreach (var armour in InvArmour)
            {
                Console.WriteLine($"{counter}. {armour.Name}");
                counter++;
            }
            counter = 1;
            Console.WriteLine("CONSUMABLES");
            foreach (var consumable in InvConsumables)
            {
                Console.WriteLine($"{counter}. {consumable.Name}");
                counter++;
            }
            Console.WriteLine();
        }

        public void TryEquipItem(ParentEquipable item)
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
                        if (wepItem.IsTwoHanded && EqWeapon["Lhand"] != null)
                        {
                            Console.WriteLine($"this weapon requires two hands to wield and so equipping it will " +
                                $"unequip the {EqWeapon["Lhand"].Name} in your left hand\n");
                            // !!ADD FUNCTIONALITY FOR A CONFIRM FOR THIS OPTION!!
                            UnequipItem("Lhand");
                        }
                        // check if equipping a left hand weapon will need to override a two handed
                        // weapon in the right hand
                        if (EqWeapon["Lhand"] != null)
                        {
                            if (wepItem.Slot == "Lhand" && EqWeapon["Lhand"].IsTwoHanded)
                            {
                                Console.WriteLine($"equipping this weapon in your left hand will uneqiup your " +
                                    $"{EqWeapon["Lhand"].Name} as this weapon required two hands to wield\n");
                                // !!ADD FUNCTIONALITY FOR A CONFIRM FOR THIS OPTION!!
                                UnequipItem("Lhand");
                            }
                        }
                        // check if there is still already an item in the desired slot that needs to be removed
                        if (EqWeapon[wepItem.Slot] != null)
                        {
                            UnequipItem(wepItem.Slot);
                        }
                        // equip the weapon
                        EquipWeapon(wepItem);
                        break;
                    case "Armour":
                        ParentArmour armItem = (ParentArmour)item;
                        // check if there is already an item in that slot that needs to be unequipped
                        if (EqArmour[armItem.Slot] != null)
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
            // change the name of the weapon now its equipped
            weapon.ChangeNameEquip();
            // add this weapons stats to the player
            CurrentAtkDmg += weapon.Attack;
            CurrentDefence += weapon.Defence;

            // equip the weapon to the right slot
            EqWeapon[weapon.Slot] = weapon;
            // also equip it in the left hand if the weapon is two handed
            if (weapon.IsTwoHanded)
            {
                EqWeapon["Lhand"] = weapon;
            }
        }

        private void EquipArmour(ParentArmour armour)
        // equips armour to the player
        {
            //change the name of the armour now its equipped
            armour.ChangeNameEquip();
            // add the armours stats to the player
            CurrentDefence += armour.Defence;

            // equip the armour
            EqArmour[armour.Slot] = armour;
        }

        public void UnequipItem(string slot)
        // unequips the item in the given slot
        {
            // weapons
            if (slot == "Rhand" | slot == "Lhand")
            {
                //change the name of the weapon now its unequipped
                EqWeapon[slot].ChangeNameUnequip();
                // remove the stats from the player
                CurrentAtkDmg -= EqWeapon[slot].Attack;
                CurrentDefence -= EqWeapon[slot].Defence;

                // check if the weapon is two handed
                if (EqWeapon[slot].IsTwoHanded)
                {
                    // if so, unequip it from both hands
                    EqWeapon["Rhand"] = null;
                    EqWeapon["Lhand"] = null;
                }
                else
                {
                    // otherwise just unequip it from the equipped hand
                    EqWeapon[slot] = null;
                }
            }
            // armour
            else if (slot == "Head" | slot == "Chest" | slot == "Legs" | slot == "Feet")
            {
                // change the name of the armour now its unequipped
                EqArmour[slot].ChangeNameUnequip();
                // remove the stats from the player
                CurrentDefence -= EqArmour[slot].Defence;

                // unequip the armour
                EqArmour[slot] = null;
            }
            else
            {
                Console.WriteLine("WARNING!! when trying to unequip an item, an invalid slot was passed." +
                    "this means the stats given by the item have not been removed from the player despite" +
                    "the item likely no longer being in the players equipped dict.");
            }
        }
    }
}