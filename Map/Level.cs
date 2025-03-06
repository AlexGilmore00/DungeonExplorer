using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Level
    // when fullt completethis class will generate a 2d array filled woth rooms depending on the difficulty
    // it willl hold info in the current active room as well as the layout of the map
    {
        public ParentRoom CurrentRoom { get; private set; }

        private int _difficulty;
        public Level(int difficulty) 
        {
            // temporary for testing
            _difficulty = difficulty;
            CurrentRoom = new DefaultRoom(_difficulty);
        }
    }
}
