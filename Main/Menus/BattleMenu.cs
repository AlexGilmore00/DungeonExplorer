using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class BattleMenu
    {
        public static void ChooseEnemyToFight(Player player, ParentRoom currentRoom)
        // entry point to the Battle Menu
        // gives the player a list of the enemies in the current room and asks the player
        // to choose one then enters combat with that enemy
        // rooms cannot contain more than 9 enemies
        {
            bool endSubroutine = false;
            while (!endSubroutine)
            {
                // keeps track of valid inputs since number of enemies can change
                HashSet<int> validInputs = new HashSet<int>();
                // print enemy list
                Console.WriteLine("which enemy would you like to fight?");
                int counter = 1;
                foreach (ParentEnemy enemy in currentRoom.Enemies)
                {
                    Console.WriteLine($"[{counter}] {enemy.Name}");
                    validInputs.Add(counter);
                    counter++;
                }
                Console.WriteLine("[r] return to previous menu\n");

                //get user input
                ConsoleKeyInfo input = Console.ReadKey(true);
                // check if user input is a number
                if (char.IsDigit(input.KeyChar))
                {
                    int numInput = int.Parse(input.KeyChar.ToString());
                    // check if the inputted number is a valid input
                    if (validInputs.Contains(numInput))
                    {
                        // make sure enemy isnt already dead
                        if (currentRoom.Enemies[numInput - 1].IsDead)
                        {
                            Console.WriteLine("this enemy is already dead\n");
                            continue;
                        }
                        // call to enter battle with the selected enemy
                        StartBattle(player, currentRoom.Enemies[numInput - 1]);
                        // make sure this subroutine ends so we return back to the main
                        // menu after the battle is complete
                        endSubroutine = true;
                    }
                    else
                    {
                        Console.WriteLine("unknown command...");
                    }
                }
                else
                // if its not a number, check if its the return character [r]
                {
                    char chinput = input.KeyChar;
                    if (char.ToLower(chinput) == char.Parse("r"))
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("unkmown command...");
                    }
                }
            }
        }

        private static void StartBattle(Player player, ParentEnemy enemy)
        {
            while (!player.IsDead && !enemy.IsDead)
            {
                Console.WriteLine($"{player.Name} has {player.Health}/{player.MaxHealth}\n" +
                    $"{enemy.Name} has {enemy.Health}/{enemy.MaxHealth} health\n");
                BeginPlayerBattleTurn(player, enemy);
                BeginEnemyBattleTurn(player, enemy);
            }
            if (enemy.IsDead)
            {
                Console.WriteLine($"you have killed {enemy.Name}\n" +
                    $"you have {player.Health}/{player.MaxHealth} health");
            }
        }

        private static void BeginPlayerBattleTurn(Player player, ParentEnemy enemy)
        // acts out the players turn during a battle with the enemy in the enemy parameter
        {
            while (true)
            {
                Console.WriteLine("what action would you like to take?\n" +
                    "[1] attack enemy\n");

                ConsoleKeyInfo input = Console.ReadKey(true);

                // return after every valid action to end turn
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        player.DealDamageTo(player, enemy, true);
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private static void BeginEnemyBattleTurn(Player player, ParentEnemy enemy)
        // not fully implemented but works while the enemies are still simple
        // once complete this should also allow enemies to choose a random move
        // from their aresenal
        {
            enemy.DealDamageTo(player, enemy, false);
        }
    }
}
