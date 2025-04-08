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
                            ChooseEnemyToFight();
                            // exit the subroutine if the player dies during the fight
                            if (_player.IsDead) { return; }
                        }
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine();
                        if (_currentRoom.Containers.Length > 0)
                        {
                            ChooseContainer();
                        }
                    break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine();
                        if (MoveRoom() == true)
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
                        if (_currentRoom.Enemies[numInput - 1].IsDead)
                        {
                            Console.WriteLine("this enemy is already dead\n");
                            continue;
                        }
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
                        Console.WriteLine("unkmown command...");
                    }
                }
            }
        }

        private void StartBattle(Player player, ParentEnemy enemy)
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

        private void BeginPlayerBattleTurn(Player player, ParentEnemy enemy)
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

        private void BeginEnemyBattleTurn(Player player, ParentEnemy enemy)
        // not fully implemented but works while the enemies are still simple
        // once complete this should also allow enemies to choose a random move
        // from their aresenal
        {
            enemy.DealDamageTo(player, enemy, false);
        }

        private void ChooseContainer()
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
                Console.WriteLine("which contianer would you like to loot?");
                int counter = 1;
                foreach (ParentContainer container in _currentRoom.Containers)
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
                        if (_currentRoom.Containers[numInput - 1].Items.Count == 0)
                        {
                            Console.WriteLine("this container is empty\n");
                            continue;
                        }
                        CheckContainer(_currentRoom.Containers[numInput - 1]);
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

        private void CheckContainer(ParentContainer container)
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
                        _player.PickUpItem(item);
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

        private bool MoveRoom()
        // displays which valid directions the player can move from this room and
        // asks them to choose which one to move to. if they confirm that they do
        // want to move, return true, if not, false.
        {
            while (true)
            {
                Test.ShowCurrentLevelLayout(_currentLevel, currentRoom: _currentRoom);
                // keep track of what player inputs should be valid
                HashSet<int> validInputs = new HashSet<int>();
                // keeps track pf which valid input corresponds to which direction
                // tuple is coords of direction relative to current room in form (x, -y)
                Dictionary<int, Tuple<int, int>> inputToDirectionMap = new Dictionary<int, Tuple<int, int>>();

                // display options to the player
                Console.WriteLine("which direction would you like to move?");
                // check which directions are valid for the current room
                if (_currentRoom.Connections.Contains('N'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(0, -1);
                    Console.WriteLine($"[{num}] North");
                }
                if (_currentRoom.Connections.Contains('E'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(1, 0);
                    Console.WriteLine($"[{num}] East");
                }
                if (_currentRoom.Connections.Contains('S'))
                {
                    // add to valid inputs
                    int num = AddToValidInputs(validInputs);
                    inputToDirectionMap[num] = Tuple.Create(0, 1);
                    Console.WriteLine($"[{num}] South");
                }
                if (_currentRoom.Connections.Contains('W'))
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
                _currentLevel.ChangeCurrentRoom(inputToDirectionMap[inputNum].Item1, inputToDirectionMap[inputNum].Item2);
                return true;
            }
        }

        private int AddToValidInputs(HashSet<int> validInputs)
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