using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Status
    {
        public readonly string Name;
        public readonly int ID;
        public int Duration;
        public readonly int Strength;
        public bool HasBeenApplied;

        public Status(string name, int id, int duration)
        // name is the name of the effect
        // duration is how many turns the effect lasts for
        {
            Name = name;
            ID = id;
            Duration = duration;
            Strength = 0;
            HasBeenApplied = false;

            // default strength values for those it applies to
            switch (id)
            {
                case 1:
                    Strength = 4;
                    break;
                case 2:
                    Strength = 5;
                    break;
            }
        }
    }
}
