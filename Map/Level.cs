﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Level
    // when fullt completethis class will generate a 2d array filled woth rooms depending on the difficulty
    // it willl hold info in the current active room as well as the layout of the map
    {
        public ParentRoom CurrentRoom { get; private set; }

        private int _difficulty;
        private ParentRoom[,] _levelLayout;
        private int _roomCount;
        private int _excessRooms;  // the number of rooms left mrant to be added if a branch is terminated early
        private static Random _rnd = new Random();
        public Level(int difficulty) 
        {
            _difficulty = difficulty;
            _levelLayout = new ParentRoom[5 + (_difficulty * 2), 5 + (_difficulty * 2)];
            GenerateLevelLayout();
        }

        private void GenerateLevelLayout()
        // ??maybe use tree data structure when making the map??
        {
            int desiredRoomCount = 8 + _difficulty * 4;
            // get coords of the centre of the level to add the root room
            int xCoord = _levelLayout.GetLength(0) / 2;  // ??maybe switch to struct for storing x,y co-ords??
            int yCoord = _levelLayout.GetLength(0) / 2;

            // !!ADD FUNCTIONALITY FOR ADDING EXCESS ROOMS;
            // IF ACTUAL ROOM COUNT IS LESS THAN DESIRED ROOM COUNT, ADD EXCESS TO EXCESS ROOM!!
            AddRooms(xCoord, yCoord, desiredRoomCount);

            // set the starting room to the root room
            CurrentRoom = _levelLayout[xCoord, yCoord];
        }

        private void AddRooms(int xCoord, int yCoord, int desBranchLength)
        // recursively add rooms to _levelLayout until desired room count is reached
        // add rooms in branch like structures branching out in randon directions from last placed room
        // x and y coord are the coords in the _levelLayout array to place the new room in
        // desBranchLength if the desired number of rooms left to add form this oint in the current branch
        {
            // base case
            if (desBranchLength <= 0)
            {
                return;
            }
            desBranchLength--;  // decrement couner as room will be added

            // add room
            _roomCount++;
            _levelLayout[xCoord, yCoord] = new DefaultRoom(_difficulty);

            // check valid spots for branches
            HashSet<int> validSpots = new HashSet<int>();  /* contains numbers corresponding to which spots are available
                                                            * 0 = south, 1 = west, 2 = north, 3 = east
                                                            * these correspond to indexes in a future array
                                                            */
            if (yCoord - 1 >= 0)
                if (_levelLayout[xCoord, yCoord - 1] == null) { validSpots.Add(0); }
            if (xCoord - 1 >= 0)
                if (_levelLayout[xCoord - 1, yCoord] == null) { validSpots.Add(1); }
            if (yCoord + 1 < _levelLayout.GetLength(0))
                if (_levelLayout[xCoord, yCoord + 1] == null) { validSpots.Add(2); }
            if (xCoord + 1 < _levelLayout.GetLength(0))
                if (_levelLayout[xCoord + 1, yCoord] == null) { validSpots.Add(3); }

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
                                                        // 0 = south, 1 = west, 2 = north, 3 = east

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

            // add the rooms in the recursive case
            for (int i = 0; i < 4; i++)
            {
                // check if a room should be added in this direction
                if (branches[i] == 1)
                {
                    // operations to get the relevant x/y change for the desired direction
                    int xChange = (i % 2) * (-2 + i);
                    int yChange = ((i + 1) % 2) * (-1 + i);
                    // start the next branch
                    AddRooms(xCoord + xChange, yCoord + yChange, desBranchLength / actualBranchCount);
                }
            }
        }

        public ParentRoom[,] GetLevelLayout()
        {
            return _levelLayout;
        }

        public int[] GetRoomCountInfo()
        {
            return new int[] { _roomCount, _excessRooms };
        }

        public void ChangeCurrentRoom(ParentRoom newRoom)
        // changes the current room to the new room
        {
            CurrentRoom = newRoom;
        }
    }
}
