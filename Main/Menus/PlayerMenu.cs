using System;
using System.Collections.Generic;
using System.Linq;
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
                "[r] return to previous menu");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        player.DisplayInventoryContents();
                        break;
                    case ConsoleKey.R:
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }
    }
}
