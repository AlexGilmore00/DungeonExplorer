using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class PlayerMenu
    {
        public static void OpenPayerMenu(Player player)
        // entry point to the player menu
        // allow the player to choose between displaying their stats
        // or accessing their inventory
        {
            while (true)
            {
                Console.WriteLine("what would you like to do?\n" +
                "[1] display player stats\n" +
                "[2] look at inventory\n" +
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
                        InventoryMenu(player);
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
            Console.WriteLine($"health: {player.Health}/{player.MaxHealth}\n" +
                $"Current Damage: {player.CurrentAtkDmg}\n" +
                $"Cuttent Defence: {player.CurrentDefence}\n");
        }

        private static void InventoryMenu(Player player)
        // allow the player to: look at their inventory,
        // access their inventory to equip items !!NEEDS ADDING!!
        // sort their inventory !!NEEDS ADDING!!
        // search their inventory for an item !!NEEDS ADDING!!LINQ!!
        {
            while(true)
            {
                Console.WriteLine("what would you like to do?\n" +
                "[1] display full inventory\n" +
                "[2] open inventory\n" +
                "[r] return to previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        player.DisplayInventoryContents();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        OpenInventory(player);
                        break;
                    case ConsoleKey.R:
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private static void OpenInventory(Player player)
        // opens the inventory so the player can look at, use, and equip items
        // gives the player the option to choose between looking at their
        // weapons, armour, and conumables
        // will dispay the chosen set of items in pages of 9 items
        {
            while (true)
            {
                Console.WriteLine("which items would you like to look at?\n" +
                "[1] weapons\n" +
                "[2] armour\n" +
                "[3] consumables\n" +
                "[r] return to previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> wepsInv = player.InvWeapons.ConvertAll(x => (ParentItem)x);
                        PresentItemList(player, wepsInv);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> armsInv = player.InvArmour.ConvertAll(x => (ParentItem)x);
                        PresentItemList(player, armsInv);
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        // convert the inventory to a list of more generic type ParentItem
                        List<ParentItem> consInv = player.InvConsumables.ConvertAll(x => (ParentItem)x); ;
                        PresentItemList(player, consInv);
                        break;
                    case ConsoleKey.R:
                        return;
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

            // add the trailing page
            pages.Add(new ParentItem[tralingPageSize]);
            for (int i = 0; i < tralingPageSize; i++)
            {
                pages[fullPageAmount][i] = itemList[counter++];
            }

            int pageNumber = 0;

            // display pages to the user
            while (true)
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
                    SelectItem(activePage[numInput - 1]);
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

        private static void SelectItem(ParentItem item)
        // menu for how you can interact with an item once selected from the inventory
        {
            Console.WriteLine(item.Name);
        }
    }
}
