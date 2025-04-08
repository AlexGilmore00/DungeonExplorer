using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player _player;
        private ParentRoom _currentRoom;
        private Level _currentLevel;
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
                else if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("please make sure your name contains at least one non-space character");
                }
                else { break; }
            }
            _player = new Player(name, 100);
            Console.WriteLine();
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
                    _currentLevel = currentLevel;
                    while (!_levelComplete)
                    {
                        // load new room
                        this._currentRoom = currentLevel.CurrentRoom;
                        EnterRoomMenu(currentLevel);
                        // end the game if the player dies
                        if (_player.IsDead)
                        {
                            Console.WriteLine("you have died");
                            return;
                        }
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
                if (_currentRoom.Containers.Length > 0)
                {
                    Console.WriteLine("[3] loot a container");
                }
                else
                {
                    Console.WriteLine("[-] there are no containers to loot");
                }
                Console.WriteLine("[4] move rooms");
                Console.WriteLine("[5] display inventory");

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
                            BattleMenu.ChooseEnemyToFight(_player, _currentRoom);
                            // exit the subroutine if the player dies during the fight
                            if (_player.IsDead) { return; }
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        if (_currentRoom.Containers.Length > 0)
                        {
                            ContainerMenu.ChooseContainer(_player, _currentRoom);
                        }
                    break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine();
                        if (RoomMoveMenu.MoveRoom(_currentLevel, _currentRoom) == true)
                        {
                            return;
                        }
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        _player.DisplayInventoryContents();
                        break;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }
    }
}