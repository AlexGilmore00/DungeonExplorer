using System;
using System.Media;
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

                //get user numeric input
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine();
                        _currentRoom.DisplayDescription();
                        break;
                    default:
                        Console.WriteLine("unknown command...");
                        break;
                }

            }
        }
    }
}