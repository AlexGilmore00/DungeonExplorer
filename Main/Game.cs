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
        public Player Player;
        public ParentRoom CurrentRoom;
        public Level CurrentLevel;
        public bool LevelComplete;
        private bool _testing;

        public Game()
        {
            LevelComplete = false;
            _testing = false;

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
            Player = new Player(name, 100, 5, 0);
            Console.WriteLine();
        }

        public Game(bool testing)
        // overload constructor to be used when testing
        {
            LevelComplete = false;
            _testing = true;
            Player = new Player("player", 100, 5, 0);
        }

        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                int levelCount = 3;  // number of levels to play before completing
                for (int level = 0; level < levelCount; level++)
                {
                    Level currentLevel = new Level(level);
                    CurrentLevel = currentLevel;
                    LevelComplete = false;
                    while (!LevelComplete)
                    {
                        // load new room
                        CurrentRoom = currentLevel.CurrentRoom;
                        if (!_testing)
                            EnterRoomMenu(currentLevel);
                        // end the game if the player dies
                        if (Player.IsDead)
                        {
                            Console.WriteLine("you have died");
                            return;
                        }
                    }
                }

                // after completing the all the levels, the game is won
                Console.WriteLine("you have won!");
                return;
            }
        }

        private void EnterRoomMenu(Level currentLevel)
        // give the player options on what they would like to do in this room
        // if the player chooses to leave the room, currentLevel.CurrentRoom will be updated
        // and then the method will return back to the main loop to load the new room
        // if the player chooses to go down to the next level, levelComplete will be true and
        // this method will return to the main loop
        {
            CurrentRoom.DisplayDescription();

            while (true)
            {
                Console.WriteLine("what do you choose to do?\n");
                Console.WriteLine("MENU\n" +
                    "[1] display room description again");
                if (CurrentRoom.Enemies.Length > 0)
                {
                    Console.WriteLine("[2] fight an enemy");
                }
                else
                {
                    Console.WriteLine("[-] there are no enemies to fight");
                }
                if (CurrentRoom.Containers.Length > 0)
                {
                    Console.WriteLine("[3] loot a container");
                }
                else
                {
                    Console.WriteLine("[-] there are no containers to loot");
                }
                Console.WriteLine("[4] move rooms");
                Console.WriteLine("[5] open player menu");

                //get user numeric input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        CurrentRoom.DisplayDescription();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine();
                        if (CurrentRoom.Enemies.Length > 0)
                        {
                            BattleMenu.ChooseEnemyToFight(Player, CurrentRoom);
                            // exit the subroutine if the player dies during the fight
                            if (Player.IsDead) { return; }
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        if (CurrentRoom.Containers.Length > 0)
                        {
                            ContainerMenu.ChooseContainer(Player, CurrentRoom);
                        }
                    break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine();
                        int retVal = RoomMoveMenu.MoveRoom(CurrentLevel, CurrentRoom, Player);
                        if (retVal == 1)
                        {
                            return;
                        }
                        if (retVal == 2)
                        {
                            LevelComplete = true;
                            return;
                        }
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.WriteLine();
                        PlayerMenu.OpenPlayerMenu(Player);
                        break;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }
            }
        }
    }
}