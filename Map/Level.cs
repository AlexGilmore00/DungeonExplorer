﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Level
    // when fullt completethis class will generate a 2d array filled woth rooms depending on the difficulty
    // it willl hold info in the current active room as well as the layout of the map
    {
        public ParentRoom CurrentRoom { get; private set; }
        public ParentRoom[,] LevelLayout { get; private set; }

        private int _difficulty;
        private int _roomCount;
        private int _excessRooms;  // the number of rooms left mrant to be added if a branch is terminated early
        private static Random _rnd = new Random();
        public Level(int difficulty) 
        {
            _difficulty = difficulty;
            LevelLayout = new ParentRoom[5 + (_difficulty * 2), 5 + (_difficulty * 2)];
            GenerateLevelLayout();
        }

        private void GenerateLevelLayout()
        // sets up information before calling the reccursive add rooms function
        // if addRooms has some branches that are terminated early, will also fill in the excess rooms
        {
            int desiredRoomCount = 8 + _difficulty * 4;
            // get coords of the centre of the level to add the root room
            int xCoord = LevelLayout.GetLength(0) / 2;
            int yCoord = LevelLayout.GetLength(0) / 2;

            // add the rooms
            // desired room count - 1 to account for the boss room
            AddRooms(xCoord, yCoord, desiredRoomCount - 1);
            if (_excessRooms > 0)
            {
                FillInExcessRooms();
            }
            AddBossRoom();

            // add the flags for room connection directions in each rooms
            SetUpRoomConnections();

            // set the starting room to the root room
            CurrentRoom = LevelLayout[yCoord, xCoord];
        }

        private void AddRooms(int xCoord, int yCoord, int desBranchLength)
        // recursively add rooms to _levelLayout until desired room count is reached
        // add rooms in branch like structures branching out in randon directions from last placed room
        // x and y coord are the coords in the _levelLayout array to place the new room in
        // desBranchLength if the desired number of rooms left to add form this point in the current branch
        {
            // base case
            if (desBranchLength <= 0)
            {
                return;
            }
            desBranchLength--;  // decrement counter as room will be added

            // add room
            _roomCount++;
            LevelLayout[yCoord, xCoord] = new DefaultRoom(_difficulty);

            // check valid spots for branches
            HashSet<int> validSpots = new HashSet<int>();  /* contains numbers corresponding to which spots are available
                                                            * 0 = south, 1 = west, 2 = north, 3 = east
                                                            * these correspond to indexes in the future 'branches' array
                                                            */
            if (yCoord - 1 >= 0)
                if (LevelLayout[yCoord - 1, xCoord] == null) { validSpots.Add(0); }
            if (xCoord - 1 >= 0)
                if (LevelLayout[yCoord, xCoord - 1] == null) { validSpots.Add(1); }
            if (yCoord + 1 < LevelLayout.GetLength(0))
                if (LevelLayout[yCoord + 1, xCoord] == null) { validSpots.Add(2); }
            if (xCoord + 1 < LevelLayout.GetLength(0))
                if (LevelLayout[yCoord, xCoord + 1] == null) { validSpots.Add(3); }

            int desBranchCount;
            // determine how many branches to create from this room
            if (validSpots.Count != 0)
            {
                desBranchCount = _rnd.Next(1, validSpots.Count);
            }
            else
            // terminate this branch if there are no valid spots
            {
                _excessRooms += desBranchLength;
                return;
            }

            int[] branches = new int[] { 0, 0, 0, 0 };  // if an indexed value is 1, add a room in that direction
                                                        // index 0 = south, 1 = west, 2 = north, 3 = east

            // populate the array with random valid directions
            int actualBranchCount = 0;
            for (int i = 0; i < desBranchCount; i++)
            {
                // choose a random valid direction
                actualBranchCount++;
                int direction = validSpots.ElementAt(_rnd.Next(validSpots.Count));
                branches[direction] = 1;  // set the flag for that direction to true
                validSpots.Remove(direction);  // remove that direction for further itterations
            }

            // find the number of rooms that will be erroniously lost because of integer
            // division when recursively calling the function
            int lostRooms = desBranchLength % actualBranchCount;

            // add the rooms in the recursive case
            for (int i = 0; i < 4; i++)
            {
                // check if a room should be added in this direction
                if (branches[i] == 1)
                {
                    // operations to get the relevant x/y change for the desired direction
                    int xChange = (i % 2) * (-2 + i);
                    int yChange = ((i + 1) % 2) * (-1 + i);
                    // distribute lost rooms - has a bias towards directions with a lower index
                    int extraRoom = 0;
                    if (lostRooms > 0)
                        extraRoom = 1;
                    lostRooms--;
                    // start the next branch
                    AddRooms(xCoord + xChange, yCoord + yChange, (desBranchLength / actualBranchCount) + extraRoom);
                }
            }
        }

        private void FillInExcessRooms()
        // find all valid room spots and add in rooms there
        // if excess rooms is still not 0 after the first call, call the function again
        {
            // find all the valid rooms
            int levelSize = LevelLayout.GetLength(0);
            // stores all the valid spots found, tuple is in format (x-coordinate, y-coordinate)
            List<Tuple<int, int>> validSpots = new List<Tuple<int, int>>();
            for (int x = 0; x <  levelSize; x++)
            {
                for (int y = 0; y < levelSize; y++)
                {
                    // check if location is alrady occupied by a room
                    if (LevelLayout[y, x] != null)
                        continue;

                    bool isValid = false;
                    // check if it has at least one connecting room
                    if (y - 1 > 0)
                        if (LevelLayout[y - 1, x] != null) { isValid = true; }
                    if (x - 1 > 0)
                        if (LevelLayout[y, x - 1] != null) { isValid = true; }
                    if (y + 1 < levelSize - 1)
                        if (LevelLayout[y + 1, x] != null) { isValid = true; }
                    if (x + 1 < levelSize - 1)
                        if (LevelLayout[y, x + 1] != null) { isValid = true; }

                    if (isValid)
                    {
                        validSpots.Add(Tuple.Create(x, y));
                    }
                }
            }

            // randomly add rooms to these valid coordinates
            while (_excessRooms > 0 && validSpots.Count() > 0)
            {
                // get a random valid spot
                Tuple<int, int> coords = validSpots[_rnd.Next(validSpots.Count())];
                validSpots.Remove(coords);
                // add the room
                LevelLayout[coords.Item2, coords.Item1] = new DefaultRoom(_difficulty);
                _roomCount++;
                _excessRooms--;
            }

            // if all excess rooms are still not added, call the function again
            if (_excessRooms > 0)
            {
                FillInExcessRooms();
            }
        }

        private void AddBossRoom()
        // add a boss room to a raandom valid location
        // cannot be adjacent to root room
        {
            // a list of tuples representing the coordinates of valid
            // locations to place a room
            // a valid location is and coordinate with at least one
            // connecting room
            // the tuple is structures as such:
            // item1 = x, item2 = y
            List<Tuple<int, int>> validSpots = new List<Tuple<int, int>>();

            // check each room to see if its a valid spot and
            // add it to the list if so
            int levelSize = LevelLayout.GetLength(0);
            for (int y = 0; y < levelSize; y++)
            {
                for (int x = 0; x < levelSize; x++)
                {
                    bool valid = false;

                    // make sure a room isnt already there
                    if (LevelLayout[y, x] != null)
                    {
                        continue;
                    }

                    // check if adjacent to root room and continue
                    // if so
                    int rootCoord = levelSize / 2;
                    if (x == rootCoord + 1 && y == rootCoord
                        || x == rootCoord - 1 && y == rootCoord
                        || y == rootCoord + 1 && x == rootCoord
                        || y == rootCoord - 1 && x == rootCoord)
                    {
                        continue;
                    }


                    // check for adjacent rooms
                    if (y - 1 >= 0)
                        if (LevelLayout[y - 1, x] != null)
                            valid = true;
                    if (x + 1 < levelSize)
                        if (LevelLayout[y, x + 1] != null)
                            valid = true;
                    if (y + 1 < levelSize)
                        if (LevelLayout[y + 1, x] != null)
                            valid = true;
                    if (x - 1 >= 0)
                        if (LevelLayout[y, x - 1] != null)
                            valid = true;

                    // add the room to valid spots if valid
                    if (valid)
                        validSpots.Add(Tuple.Create(x, y));
                }
            }

            // add the boss room
            Tuple<int, int> coords = validSpots[_rnd.Next(0, validSpots.Count)];
            LevelLayout[coords.Item2, coords.Item1] = new BossRoom(_difficulty);
            _roomCount++;
        }

        private void SetUpRoomConnections()
        // go through each room in _levelLayout and add all the valid connecting
        // directions to each room
        {
            int levelSize = LevelLayout.GetLength(0);
            // check each room
            for (int y = 0; y < levelSize; y++)
            {
                for (int x = 0; x < levelSize; x++)
                {
                    // check if no room is there
                    if (LevelLayout[y, x] == null)
                        continue;

                    // check for adjacent rooms
                    if (y - 1 >= 0)
                        if (LevelLayout[y - 1, x] != null)
                            LevelLayout[y, x].AddConnection('N');
                    if (x + 1 < levelSize)
                        if (LevelLayout[y, x + 1] != null)
                            LevelLayout[y, x].AddConnection('E');
                    if (y + 1 < levelSize)
                        if (LevelLayout[y + 1, x] != null)
                            LevelLayout[y, x].AddConnection('S');
                    if (x - 1 >= 0)
                        if (LevelLayout[y, x - 1] != null)
                            LevelLayout[y, x].AddConnection('W');

                    // add down connection to boss rooms
                    if (LevelLayout[y, x] is BossRoom)
                        LevelLayout[y, x].AddConnection('D');
                }
            }
        }

        public void ChangeCurrentRoom(int x, int y)
        // changes the current room to a new room based on the relative change
        // in coords given relative to the current room
        {
            Tuple<int, int> currentRoomCoords = GetCurrentRoomCoords();
            CurrentRoom = LevelLayout[currentRoomCoords.Item2 + y, currentRoomCoords.Item1 + x];
        }

        private Tuple<int, int> GetCurrentRoomCoords()
        // returns the coords of the current room in the form of a tuple in form (x, y)
        {
            int levelSize = LevelLayout.GetLength(0);
            // find the room
            for (int x = 0; x < levelSize; x++)
            {
                for (int y = 0;  y < levelSize; y++)
                {
                    if (LevelLayout[y, x] == CurrentRoom)
                    {
                        return Tuple.Create(x, y);
                    }
                }
            }

            // if the room is somehow not found, default to root room
            Console.WriteLine("WARNING!! the current room was not found when searching in" +
                "_levelLayout. default coords corresponding to the root room were returned.");
            return Tuple.Create(levelSize / 2, levelSize / 2);
        }

        public int[] GetRoomCountInfo()
        {
            return new int[] { _roomCount, _excessRooms };
        }
    }
}
