using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ContainerMenu
    {
        public static void ChooseContainer(Player player, ParentRoom currentRoom)
        // entry point for the Container Menu
        // gives the player a list of containers in the room and prompts them to
        // check one of them then calls to check that container
        // rooms can't have more than 9 containers
        {
            bool endSubroutine = false;
            while (!endSubroutine)
            {
                // keep track of valid inputs since number of containers can change
                HashSet<int> validInputs = new HashSet<int>();
                // display the list of containers in the room
                Console.WriteLine("which container would you like to loot?");
                int counter = 1;
                foreach (ParentContainer container in currentRoom.Containers)
                {
                    Console.WriteLine($"[{counter}] {container.Name}");
                    validInputs.Add(counter);
                    counter++;
                }
                Console.WriteLine("[r] return to previous menu\n");

                // get user input
                ConsoleKeyInfo input = Console.ReadKey(true);
                // check if the user input is a number
                if (char.IsDigit(input.KeyChar))
                {
                    int numInput = int.Parse(input.KeyChar.ToString());
                    // check if the inputted number is a valid input
                    if (validInputs.Contains(numInput))
                    {
                        // chack if the container is empty
                        if (currentRoom.Containers[numInput - 1].Items.Count == 0)
                        {
                            Console.WriteLine("this container is empty\n");
                            continue;
                        }
                        CheckContainer(player, currentRoom.Containers[numInput - 1]);
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
                        Console.WriteLine("unknown command...");
                    }
                }
            }
        }

        private static void CheckContainer(Player player, ParentContainer container)
        // print what items the container contains and give the player the option
        // to take them
        {
            Console.WriteLine($"this {container.Name} contains:");
            foreach (ParentItem item in container.Items)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();

            Console.WriteLine("what would you like to do?\n" +
                "[1] take all items\n" +
                "[r] return to the previous menu\n");
            // get user inpur
            ConsoleKeyInfo input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    foreach (ParentItem item in container.Items)
                    {
                        player.PickUpItem(item);
                    }
                    container.Items.Clear();
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
