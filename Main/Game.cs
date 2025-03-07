using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player _player;
        private ParentRoom _currentRoom;
        private bool _levelComplete;

        public Game()
        {
            // setup player character
            string name;
            Console.WriteLine("what would you like to be called");
            while (true)
            {
                name = Console.ReadLine();
                if (name.Length > 16)
                {
                    Console.WriteLine("please enter a name containing 16 characters or less");
                }
                else { break; }
            }
            _player = new Player(name, 100);
        }
        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                int levelCount = 3;  // number of levels to play before completing
                this._levelComplete = false;
                for (int level = 0; level < levelCount; level++)
                {
                    Level currentLevel = new Level(level);
                    while (!_levelComplete)
                    {
                        // load new room
                        this._currentRoom = currentLevel.CurrentRoom;
                        EnterRoomMenu(currentLevel);
                    }
                }
            }
        }

        private void EnterRoomMenu(Level currentLevel)
        // give the player options on what they would like to do in this room
        // if the player chooses to leave the room, currentLevel.CurrentRoom will be updated
        // and then the method will return back to the main loop to load the new room
        // if the player chooses to go down to the next level, levelComplete will be true and
        // this method will return to the main loop
        {
            _currentRoom.DisplayDescription();

            while (true)
            {
                Console.WriteLine("what do you choose to do?\n");
                Console.WriteLine("MENU\n" +
                    "[1] display room description again");
                if (_currentRoom.Enemies.Length > 0)
                {
                    Console.WriteLine("[2] fight an enemy");
                }
                else
                {
                    Console.WriteLine("[-] there are no enemies to fight");
                }

                //get user numeric input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        _currentRoom.DisplayDescription();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        if (_currentRoom.Enemies.Length > 0)
                        {
                            ChooseEnemyToFight();
                        }
                        break;
                    // !!FOR TESTING ONLY!!
                    // !!REMOVE AFTER USE!!
                    case ConsoleKey.G:
                        Test.GenerateNewRoom(currentLevel, 0);
                        return;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }

        private void ChooseEnemyToFight()
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
                foreach (ParentEnemy enemy in _currentRoom.Enemies)
                {
                    Console.WriteLine($"[{counter}] {enemy.Name}");
                    validInputs.Add(counter);
                    counter++;
                }
                Console.WriteLine("[r] return to previous menu");

                //get user input
                ConsoleKeyInfo input = Console.ReadKey(true);
                // check if user input is a number
                if (char.IsDigit(input.KeyChar))
                {
                    int numInput = int.Parse(input.KeyChar.ToString());
                    // check if the inputted number is a valid input
                    if (validInputs.Contains(numInput))
                    {
                        // call to enter battle with the selected enemy
                        StartBattle(_player, _currentRoom.Enemies[numInput - 1]);
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
                        Console.WriteLine("unkmown command");
                    }
                }
            }
        }

        private void StartBattle(Player player, ParentEnemy enemy)
        {
            Console.WriteLine(player.Name);
            Console.WriteLine(enemy.Name);
        }
    }
}