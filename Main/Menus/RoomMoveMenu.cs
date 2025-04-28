using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class RoomMoveMenu
    {
        public static int MoveRoom(Level currentLevel, ParentRoom currentRoom)
        // entry point to Room Move Menu
        // displays which valid directions the player can move from this room and
        // asks them to choose which one to move to. if the confirm they want to
        // move to another room on the same level: return 1
        // if they confirm they want to move to the next level: return 2
        // if they dont want to move: return 0
        {
            while (true)
            {
                DisplayLevelLayout(currentLevel, currentRoom);
                // keep track of what player inputs should be valid
                HashSet<int> validInputs = new HashSet<int>();
                // keeps track of which valid input corresponds to which direction
                // tuple is coords of direction relative to current room in form (x, -y)
                // a value of (2, 0) denotes going down
                Dictionary<int, Tuple<int, int>> inputToDirectionMap = new Dictionary<int, Tuple<int, int>>();

                // display options to the player
                Console.WriteLine("\nwhich direction would you like to move?");
                // check which directions are valid for the current room
                if (currentRoom.Connections.Contains('N'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(0, -1);
                    Console.WriteLine($"[{num}] North");
                }
                if (currentRoom.Connections.Contains('E'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(1, 0);
                    Console.WriteLine($"[{num}] East");
                }
                if (currentRoom.Connections.Contains('S'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(0, 1);
                    Console.WriteLine($"[{num}] South");
                }
                if (currentRoom.Connections.Contains('W'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(-1, 0);
                    Console.WriteLine($"[{num}] West");
                }
                if (currentRoom.Connections.Contains('D'))
                {
                    // check if the boss has been killed
                    if (currentRoom.Enemies.Any(e => e.IsDead == false))
                    {
                        Console.WriteLine("[-] The way doen is currently blocked");
                    }
                    else
                    {
                        int num = AddToValidInputs(validInputs);
                        inputToDirectionMap[num] = Tuple.Create(2, 0);
                        Console.WriteLine($"[{num}] Down");
                    }
                }
                Console.WriteLine("[r] return to the previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                // check if the input if the return character
                if (char.ToLower(input.KeyChar) == 'r')
                {
                    return 0;
                }
                // make sure the input is a number
                if (!char.IsDigit(input.KeyChar))
                {
                    Console.WriteLine("unknown command...");
                    continue;
                }
                // make sure the input is a recognised as a valid input
                int inputNum = int.Parse(input.KeyChar.ToString());
                if (!validInputs.Contains(inputNum))
                {
                    Console.WriteLine("unknown command...");
                    continue;
                }
                // if input is valid, carry on
                // check to see if they want to move down
                if (inputToDirectionMap[inputNum].Item1 == 2)
                {
                    return 2;
                }
                // if not, move room
                currentLevel.ChangeCurrentRoom(inputToDirectionMap[inputNum].Item1, inputToDirectionMap[inputNum].Item2);
                return 1;
            }
        }

        private static int AddToValidInputs(HashSet<int> validInputs)
        // takes a hashset and adds its max value + 1 to it
        // if it is empty, adds 1
        // returns the number that was added
        {
            int num;
            if (validInputs.Count != 0)
            {
                num = validInputs.Max() + 1;
                validInputs.Add(num);
                return num;
            }
            else
            {
                num = 1;
                validInputs.Add(num);
                return num;
            }
        }

        private static void DisplayLevelLayout(Level currentLevel, ParentRoom currentRoom)
        // displays the current layout of the current level to the user, the room the user
        // is currently in, and a number grid for easier use
        {
            ParentRoom[,] levelLayout = currentLevel.LevelLayout;

            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                // display y axis numbers
                Console.Write($"{i}    ");
                for (int j = 0; j < levelLayout.GetLength(0); j++)
                {
                    // display room status
                    string room;
                    if (levelLayout[i, j] == currentRoom)
                        room = "[ i ]";
                    else if (levelLayout[i, j] is BossRoom)
                        room = "[>:(]";
                    else if (levelLayout[i, j] != null)
                        room = "[   ]";
                    else
                        room = "#~#~#";
                    Console.Write($"{room}");
                }
                Console.WriteLine();
            }

            Console.Write("     ");
            // display x axis numbers
            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                Console.Write($"{i}    ");
            }
        }
    }
}
