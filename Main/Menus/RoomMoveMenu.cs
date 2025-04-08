using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class RoomMoveMenu
    {
        public static bool MoveRoom(Level currentLevel, ParentRoom currentRoom)
        // entry point to Room Move Menu
        // displays which valid directions the player can move from this room and
        // asks them to choose which one to move to. if they confirm that they do
        // want to move, return true, if not, false.
        {
            while (true)
            {
                Test.ShowCurrentLevelLayout(currentLevel, currentRoom: currentRoom);
                // keep track of what player inputs should be valid
                HashSet<int> validInputs = new HashSet<int>();
                // keeps track pf which valid input corresponds to which direction
                // tuple is coords of direction relative to current room in form (x, -y)
                Dictionary<int, Tuple<int, int>> inputToDirectionMap = new Dictionary<int, Tuple<int, int>>();

                // display options to the player
                Console.WriteLine("which direction would you like to move?");
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
                Console.WriteLine("[r] return to the previous menu\n");

                // get player input
                ConsoleKeyInfo input = Console.ReadKey(true);

                // check if the input if the return character
                if (char.ToLower(input.KeyChar) == 'r')
                {
                    return false;
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
                // if input is valid, move to the next room
                currentLevel.ChangeCurrentRoom(inputToDirectionMap[inputNum].Item1, inputToDirectionMap[inputNum].Item2);
                return true;
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
    }
}
