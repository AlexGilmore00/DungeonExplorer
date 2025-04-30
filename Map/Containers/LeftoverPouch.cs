using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LeftoverPouch : ParentContainer
    {
        public LeftoverPouch()
        {
            Name = "Leftover Pouch";
            // set up the number of items in the container
            Items = new List<ParentItem>();
            PopulateItems(Rnd.Next(1, 2), 5);
        }
    }
}
