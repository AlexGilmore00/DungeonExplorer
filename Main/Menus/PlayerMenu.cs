using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class PlayerMenu
    {
        public static void OpenPlayerMenu(Player player)
        // entry point to the player menu
        // allow the player to choose between displaying their stats
        // or manipulating their inventory
        // can access inventory to equip/use items
        // can sort inventory
        // can search inventory !!NEEDS ADDING!!LINQ!!
        {
            while (true)
            {
                Console.WriteLine("what would you like to do?\n" +
                "[1] display player stats\n" +
                "[2] display full inventory\n" +
                "[3] open inventory\n" +
                "[4] sort inventory\n" +
                "[5] search inventory\n" +
                "[r] return to previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        DisplayPlayerStats(player);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        player.DisplayInventoryContents();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        List<ParentItem> invList = ChooseCategory(player);
                        PresentItemList(player, invList);
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine();
                        List<ParentItem> invListSort = ChooseCategory(player);
                        SortInventory(player, invListSort);
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.WriteLine();
                        SearchInventory(player);
                        break;
                    case ConsoleKey.R:
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private static void DisplayPlayerStats(Player player)
        // display the players stats
        {
            // set up the players equipped items
            string head;
            if (player.EqArmour["Head"] != null) { head = player.EqArmour["Head"].Name;  } else { head = "nothing"; }
            string chest;
            if (player.EqArmour["Chest"] != null) { chest = player.EqArmour["Chest"].Name; } else { chest = "nothing"; }
            string legs;
            if (player.EqArmour["Legs"] != null) { legs = player.EqArmour["Legs"].Name; } else { legs = "nothing"; }
            string feet;
            if (player.EqArmour["Feet"] != null) { feet = player.EqArmour["Feet"].Name; } else { feet = "nothing"; }
            string rhand;
            if (player.EqWeapon["Rhand"] != null) { rhand = player.EqWeapon["Rhand"].Name; } else { rhand = "nothing"; }
            string lhand;
            if (player.EqWeapon["Lhand"] != null) { lhand = player.EqWeapon["Lhand"].Name; } else { lhand = "nothing"; }


            Console.WriteLine($"name: {player.Name}\n" +
                $"health: {player.Health}/{player.MaxHealth}\n" +
                $"Current Damage: {player.CurrentAtkDmg}\n" +
                $"Cuttent Defence: {player.CurrentDefence}\n" +
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
            foreach (var status in player.StatusEffects)
            {
                Console.WriteLine($"{status.Name}: {status.Duration} turns");
            }
            Console.WriteLine();
        }

        private static List<ParentItem> ChooseCategory(Player player)
        // gives the player the option to choose between looking at their
        // weapons, armour, and conumables
        // returns all the items in that section of their inventory as
        // a list of generic ParentItem
        {
            while (true)
            {
                Console.WriteLine("which items would you like to look at?\n" +
                "[1] weapons\n" +
                "[2] armour\n" +
                "[3] consumables\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> wepsInv = player.InvWeapons.ConvertAll(x => (ParentItem)x);
                        return wepsInv;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> armsInv = player.InvArmour.ConvertAll(x => (ParentItem)x);
                        return armsInv;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> consInv = player.InvConsumables.ConvertAll(x => (ParentItem)x); ;
                        return consInv;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private static void PresentItemList(Player player, List<ParentItem> itemList)
        // displays the given item list to the player in pages of 9 items and allows them
        // to select an item to either inspect or use/equip it
        {
            // if there are no items in item list, say so and return
            if (itemList.Count <= 0)
            {
                Console.WriteLine("you have no items in this category\n");
                return;
            }

            // a list containing all the pages of items
            List<ParentItem[]> pages = new List<ParentItem[]>();
            int fullPageAmount = itemList.Count / 9;  // the number of full pages
            int tralingPageSize = itemList.Count % 9;  // the number of items in the last page

            // fill the page list
            int counter = 0;
            for (int page = 0; page < fullPageAmount; page++)
            {
                pages.Add(new ParentItem[9]);

                // populate the page
                for (int i = 0; i < 9; i++)
                {
                    pages[page][i] = itemList[counter++];
                }
            }

            // add the trailing page if necesarry
            if (tralingPageSize > 0)
            {
                pages.Add(new ParentItem[tralingPageSize]);
                for (int i = 0; i < tralingPageSize; i++)
                {
                    pages[fullPageAmount][i] = itemList[counter++];
                }
            }

            int pageNumber = 0;
            bool endSubroutine = false;
            // display pages to the user
            while (!endSubroutine)
            {
                // set the current active page
                ParentItem[] activePage = pages[pageNumber];

                Console.WriteLine("choose an item to interact with");
                DisplayPage(activePage, pageNumber, pages.Count);
                Console.WriteLine();
                if (pageNumber > 0)
                {
                    Console.WriteLine("[p] previous page");
                }
                if (pageNumber < pages.Count - 1)
                {
                    Console.WriteLine("[n] next page");
                }
                Console.WriteLine("[r] return to previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                //check if the input is a number
                if (char.IsDigit(input.KeyChar))
                {
                    int numInput = int.Parse(input.KeyChar.ToString());

                    // make sure the number inputted coresponds to a valid item on the page
                    if (numInput < 1 || numInput > activePage.Length)
                    {
                        Console.WriteLine("unknown command...");
                        continue;
                    }
                    // if its a valid number, select the item
                    endSubroutine = SelectItem(player, activePage[numInput - 1]);
                    
                }
                else
                // if the item is not a number, sheck if it is another valid char input
                {
                    char chinput = input.KeyChar;
                    if (char.ToLower(chinput) == 'r')
                    {
                        return;
                    }
                    else if (char.ToLower(chinput) == 'p' && pageNumber > 0)
                    {
                        pageNumber--;
                    }
                    else if (char.ToLower(chinput) == 'n' && pageNumber < pages.Count - 1)
                    {
                        pageNumber++;
                    }
                    else
                    {
                        Console.WriteLine("unknown command...");
                    }
                }
            }
        }

        private static void DisplayPage(ParentItem[] page, int pageNumber, int totalPages)
        // displays the items in the active page to the user
        {
            Console.WriteLine($"PAGE {pageNumber + 1}/{totalPages}");
            int counter = 1;
            foreach (ParentItem item in page)
            {
                Console.WriteLine($"[{counter++}] {item.Name}");
            }
        }

        private static bool SelectItem(Player player, ParentItem item)
        // menu for how you can interact with an item once selected from the inventory
        // can inspect all items
        // can equip equipables
        // can use consumables !!NEEDS ADDING!!
        // returns true to send the player back to the OpenPlayerMenu
        // returns false to send the player back to PresentItemList
        {
            while (true)
            {
                Console.WriteLine($"what would you like to do with: {item.Name}?\n" +
                    $"[1] inspect item");
                if (item is ParentEquipable)
                {
                    Console.WriteLine("[2] equip item");
                }
                else if (item is ParentConsumable)
                {
                    Console.WriteLine("[2] use item");
                }
                // check if the item is equipped and gove the option to unequip if necessary
                if (item is ParentEquipable)
                {
                    ParentEquipable eqItem = (ParentEquipable)item;
                    if ((eqItem is ParentWeapon && player.EqWeapon[eqItem.Slot] == item)
                        || (eqItem is ParentArmour && player.EqArmour[eqItem.Slot] == item))
                    {
                        Console.WriteLine("[3] unequip item");
                    }
                }
                Console.WriteLine("[r] return to previous menu");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        item.DisplayDescription();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        if (item is ParentEquipable)
                        {
                            player.TryEquipItem((ParentEquipable)item);
                            Console.WriteLine($"{item.Name} was equipped");
                            return false;
                        }
                        else if (item is ParentConsumable)
                        {
                            ParentConsumable conItem = (ParentConsumable)item;
                            conItem.UseItem(player);
                            player.PickUpItem(item, remove: true);
                            // must return to OpenInventory to reset the pages in
                            // PresentItemList after an item has been removed
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("unknown command...");
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        if (item is ParentEquipable)
                        {
                            ParentEquipable eqItem = (ParentEquipable)item;
                            if ((eqItem is ParentWeapon && player.EqWeapon[eqItem.Slot] == item)
                                || (eqItem is ParentArmour && player.EqArmour[eqItem.Slot] == item))
                            {
                                player.UnequipItem(eqItem.Slot);
                            }
                            Console.WriteLine($"you unequipped {item.Name}");
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("unknown commend...");
                        }
                        break;
                    case ConsoleKey.R:
                        return false;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private static void SortInventory(Player player, List<ParentItem> itemList)
        // allows the player to sort their inventory based off differing variables
        // all items in itemList should be of the same category
        {
            // if there are no items in item list, say so and return
            if (itemList.Count <= 0)
            {
                Console.WriteLine("you have no items in this category\n");
                return;
            }


            while (true)
            {
                // give different options depending on what category the items are
                Console.WriteLine("how would you like to sort these items?\n" +
                    "[1] name ascending\n" +
                    "[2] name descending");
                if (itemList[0].Category == "Weapon" || itemList[0].Category == "Armour")
                {
                    Console.WriteLine("[3] defence ascending\n" +
                        "[4] defence descending");
                }
                if (itemList[0].Category == "Weapon")
                {
                    Console.WriteLine("[5] attack ascending\n" +
                        "[6] attack descending");
                }
                Console.WriteLine("[r] return to previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                // setup needed lists
                List<ParentItem> sortedList = new List<ParentItem>();
                List<ParentEquipable> tempEquippableList = new List<ParentEquipable>();
                List<ParentWeapon> tempWepList = new List<ParentWeapon>();

                // use player input
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        itemList.Sort((a, b) => (a.Name.CompareTo(b.Name)));
                        sortedList = itemList;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        itemList.Sort((a, b) => (a.Name.CompareTo(b.Name)));
                        itemList.Reverse();
                        sortedList = itemList;
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        if (itemList[0].Category == "Weapon" || itemList[0].Category == "Armour")
                        {
                            tempEquippableList = itemList.ConvertAll(x => (ParentEquipable)x);
                            sortedList = tempEquippableList.OrderBy(x => x.Defence).ToList()
                                .ConvertAll(x => (ParentItem)x);
                        }
                        else
                        {
                            Console.WriteLine("unknown command...");
                            continue;
                        }
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        if (itemList[0].Category == "Weapon" || itemList[0].Category == "Armour")
                        {
                            tempEquippableList = itemList.ConvertAll(x => (ParentEquipable)x);
                            sortedList = tempEquippableList.OrderByDescending(x => x.Defence).ToList()
                                .ConvertAll(x => (ParentItem)x);
                        }
                        else
                        {
                            Console.WriteLine("unknown command...");
                            continue;
                        }
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        if (itemList[0].Category == "Weapon")
                        {
                            tempWepList = itemList.ConvertAll(x => (ParentWeapon)x);
                            sortedList = tempWepList.OrderBy(x => x.Attack).ToList()
                                .ConvertAll(x => (ParentItem)x);
                        }
                        else
                        {
                            Console.WriteLine("unknown command...");
                            continue;
                        }
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        if (itemList[0].Category == "Weapon")
                        {
                            tempWepList = itemList.ConvertAll(x => (ParentWeapon)x);
                            sortedList = tempWepList.OrderByDescending(x => x.Attack).ToList()
                                .ConvertAll(x => (ParentItem)x);
                        }
                        else
                        {
                            Console.WriteLine("unknown command...");
                            continue;
                        }
                        break;
                    case ConsoleKey.R:
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        continue;
                }

                // set the players ionventory to the sorted list
                player.SetInventory(sortedList, sortedList[0].Category);
                return;
            }
        }

        private static void SearchInventory(Player player)
        // allows the player to input a string and search their
        // inventory for items containing that string in their name
        // creates a list of all items containing that string and then
        // calls PresentItemList with that list as losng as it is not
        // empty
        // list may contain items of multiple didfferent categories
        {
            // get the players full inventory
            List<ParentItem> fullInv = player.InvWeapons.ConvertAll(x => (ParentItem)x)
                .Concat(player.InvArmour.ConvertAll(x => (ParentItem)x))
                .Concat(player.InvConsumables.ConvertAll(x => (ParentItem)x))
                .ToList();

            // get the string the player wants to search for
            Console.WriteLine("enter the string you want to use to search...");
            string input = Console.ReadLine().ToLower();

            // get the item list
            List<ParentItem> searchedList = fullInv
                .Where(item => item.Name.ToLower().Contains(input))
                .Select(item => item)
                .ToList();
            
            // present the searched for items to the player if the list
            // isnt empty
            if (searchedList.Count > 0)
            {
                PresentItemList(player, searchedList);
            }
            else
            {
                Console.WriteLine($"no items found containing '{input}' in the name");
            }

        }
    }
}
